using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public TMP_Text moneyBar;
    public Button soundButton;
    public Sprite soundOn;
    public Sprite soundOff;
    public TMP_Text winBar;
    public TMP_Text bidBar;
    public Image fillBar;
    private bool isAudio;
    public void RaiseBid()
    {
        int tempBid = GameManager.instance.GetCurrentBid() + 10;
        if (tempBid<Player.instance.money)
        GameManager.instance.SetBid(tempBid);
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void TurnAudio()
    {
        if (!isAudio)
        {
            AudioListener.volume = 0;
            soundButton.GetComponent<Image>().sprite = soundOff;
            isAudio = true;
        }
        else
        {
            soundButton.GetComponent<Image>().sprite = soundOn;
            AudioListener.volume = 1f;
            isAudio = false;
        }
    }    
    private void Update()
    {
        bidBar.text = GameManager.instance.GetCurrentBid().ToString() + ",00 DEM";
        moneyBar.text = Player.instance.GetMoney().ToString()+",00 DEM";
        winBar.text = GameManager.instance.GetCurrentWin() + ",00 DEM";
        if (GameManager.instance.GetCurrentScore()==0)
        {
            fillBar.fillAmount = 0;
        }
        if (GameManager.instance.GetCurrentScore() == 1)
        {
            fillBar.fillAmount = 0.2f;
        }
        if (GameManager.instance.GetCurrentScore() == 2)
        {
            fillBar.fillAmount = 0.4f;
        }
        if (GameManager.instance.GetCurrentScore() == 3)
        {
            fillBar.fillAmount = 0.6f;
        }
        if (GameManager.instance.GetCurrentScore() == 4)
        {
            fillBar.fillAmount = 0.8f;
        }
        if (GameManager.instance.GetCurrentScore() == 5)
        {
            fillBar.fillAmount = 1f;
        }
    }
}
