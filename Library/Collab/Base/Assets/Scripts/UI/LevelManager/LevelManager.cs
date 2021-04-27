using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int Coins { get; set; }
    private int _currentMaxLevel;
    [SerializeField] private int coinsTarget;
    private int _currentMoney;
    private EnemyMain enemyMain;

    private void Start()
    {
        StartLevelManager();
        
    }
    public virtual void StartLevelManager()
    {
        _currentMoney = PlayerPrefs.GetInt("Money");
        Coins = 0;
        _currentMaxLevel = PlayerPrefs.GetInt("LevelQuantity");
        enemyMain = GameObject.FindGameObjectWithTag("Enemies").GetComponent<EnemyMain>();
    }

    private void Update()
    {
        UpdateLevelManager();
    }

    public virtual void UpdateLevelManager()
    {
        if (Coins != coinsTarget && enemyMain.AliveCount() != 0) return;
        LevelFinish();
        PlayerPrefs.SetInt("Money", _currentMoney += Coins);
    }

    private void LevelFinish()
    {
        if (_currentMaxLevel == (SceneManager.GetActiveScene().buildIndex - 1))
        {
            PlayerPrefs.SetInt("LevelQuantity", _currentMaxLevel += 1);
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            SceneManager.LoadScene("Map");
        }
    }
}
