using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int money;
    public static Player instance;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        instance = this;
        if (!PlayerPrefs.HasKey("money"))
        {
            money = 1000;
            PlayerPrefs.SetInt("money", money);
            PlayerPrefs.Save();
        }
        else
        {
            money = PlayerPrefs.GetInt("money");
        }
    }
    public void RaiseMoney(int tempMoney)
    {
        money += tempMoney;
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.Save();
    }
    public void MinusMoney(int tempMoney)
    {
        if (money-tempMoney>0)
        money -= tempMoney;
        else
        {
            money = 0;
        }
        PlayerPrefs.SetInt("money", money);
        PlayerPrefs.Save();
    }
    public int GetMoney()
    {
        return money;
    }
}
