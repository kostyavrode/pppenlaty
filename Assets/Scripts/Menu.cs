using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Menu : MonoBehaviour
{
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
    public void SelectLevel(int l)
    {
        PlayerPrefs.SetInt("Level", l);
    }
}
