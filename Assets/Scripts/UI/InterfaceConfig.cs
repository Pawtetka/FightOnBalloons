using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceConfig : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentCoins;
    [SerializeField] private LevelManager levelManager;
    private int _currentMaxLevel;

    private void Awake()
    {
        Time.timeScale = 1f;
    }
    private void Update()
    {
        if (levelManager != null)
        {
            currentCoins.text = levelManager.Coins.ToString();
        }
    }

    public void Restart()
    {
        if (levelManager != null)
        {
            levelManager.inGame = false;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void GoToMap()
    {
        if (levelManager != null)
        {
            levelManager.inGame = false;
        }
        SceneManager.LoadScene("Map");
    } 
}
