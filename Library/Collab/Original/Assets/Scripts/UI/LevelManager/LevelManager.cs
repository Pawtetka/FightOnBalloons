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
    private float _startTime;
    private float _levelTime;
    [SerializeField] private float threeStarsLevelTime;


    private void Start()
    {
        StartLevelManager();
        _startTime = Time.time;
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
    }

    private void LevelFinish()
    {
        PlayerPrefs.SetInt("Money", _currentMoney += Coins);
        PlayerPrefs.SetInt("EarnedMoney", Coins);
        _levelTime = Time.time - _startTime;
        if (_levelTime < threeStarsLevelTime)
        {
            PlayerPrefs.SetInt("StarsCount", 3);
        }
        else if (_levelTime < threeStarsLevelTime * 2)
        {
            PlayerPrefs.SetInt("StarsCount", 2);
        }
        else if ()
        {

        }
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
