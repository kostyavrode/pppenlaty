using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levels;

    private void Awake()
    {
        int g = PlayerPrefs.GetInt("Level");
        levels[g].SetActive(true);
    }
}
