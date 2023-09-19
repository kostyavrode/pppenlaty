using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Menu : MonoBehaviour
{
    UniWebView uniWebView;
    public string privacyUrl;
    public string donateUrl;
    public TMP_Text moneyBar;
    public void OnEnable()
    {
        GetPlayerMoney();
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GetPlayerMoney()
    {
        if (PlayerPrefs.HasKey("money"))
        moneyBar.text = PlayerPrefs.GetInt("money").ToString() + ",00 DEM";
        else
        {

        }
    }
    public void LoadPrivacyPolicy()
    {
        OpenWebview(privacyUrl);
    }
    public void LoadDonate()
    {
        OpenWebview(donateUrl);
    }
    private void OpenWebview(string url)
    {
        var webviewObject = new GameObject("UniWebview");
        uniWebView = webviewObject.AddComponent<UniWebView>();
        uniWebView.Frame = new Rect(0, 0, Screen.width, Screen.height);
        uniWebView.SetShowToolbar(true,false,true,true);
        uniWebView.Load(url);
        uniWebView.Show();
    }
}
