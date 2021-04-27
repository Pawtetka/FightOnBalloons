using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public bool inGame { get; set; } = true;
    private string _musicOnBackName;


    private void Start()
    {
        StartLevelManager();
        _startTime = Time.time;
        if (threeStarsLevelTime == 0)
        {
            threeStarsLevelTime = 10;
        }
    }
    
    public virtual void StartLevelManager()
    {
        Coins = 0;
        //_currentMoney = PlayerPrefs.GetInt("Money");
        _currentMaxLevel = PlayerPrefs.GetInt("LevelQuantity");
        enemyMain = GameObject.FindGameObjectWithTag("Enemies").GetComponent<EnemyMain>();
        _musicOnBackName = (SceneManager.GetActiveScene().name.Contains("Boss")
            ? SceneManager.GetActiveScene().name
            : "LevelMusic");
    }

    public void BackMusicPlay()
    {
        if (FindObjectOfType<AudioManager>() != null)
        {
            if (!inGame)
            {
                FindObjectOfType<AudioManager>().Stop(_musicOnBackName);
                return;
            }
            if ((SceneManager.GetActiveScene().name.Any(char.IsDigit)
                || SceneManager.GetActiveScene().name.Contains("Boss"))
                && !FindObjectOfType<AudioManager>().FindAudioSource(_musicOnBackName).source.isPlaying)
            {
                FindObjectOfType<AudioManager>().Play(_musicOnBackName);
            }
            else if ((SceneManager.GetActiveScene().name.Any(char.IsDigit)
                      || SceneManager.GetActiveScene().name.Contains("Boss"))
                     && FindObjectOfType<AudioManager>().FindAudioSource(_musicOnBackName).source.isPlaying)
            {
                return;
            }
        }
    }
    

    private void Update()
    {
        if (inGame)
        {
            UpdateLevelManager();
        }
        BackMusicPlay();
    }

    public virtual void UpdateLevelManager()
    {
        if (Coins != coinsTarget && enemyMain.AliveCount() != 0) return;
        inGame = false;
        BackMusicPlay();
        Invoke("LevelFinish", 0.75f);
    }

    private void LevelFinish()
    {
        //PlayerPrefs.SetInt("Money", _currentMoney += Coins);
        PlayerPrefs.SetInt("EarnedMoney", Coins);
        _levelTime = Time.time - _startTime;
        /*PlayerPrefs.SetFloat("Level" + (SceneManager.GetActiveScene().
                buildIndex - 1).ToString() + "LevelTime", _levelTime);*/
        StarsUpdate();
        if (_currentMaxLevel == (SceneManager.GetActiveScene().buildIndex - 1))
        {
            PlayerPrefs.SetInt("LevelQuantity", _currentMaxLevel += 1);
        }
        SceneManager.LoadScene("WinScene");
    }

    private void StarsUpdate()
    {
        int currentStarsCount = 0;
        /*int bestStarsCount = PlayerPrefs.GetInt("Level" + (SceneManager.GetActiveScene().
                buildIndex - 1).ToString() + "BestStarsCount");*/

        if (_levelTime < threeStarsLevelTime)
        {
            currentStarsCount = 3;
        }
        else if (_levelTime < threeStarsLevelTime * 2)
        {
            currentStarsCount = 2;
        }
        else
        {
            currentStarsCount = 1;
        }

        PlayerPrefs.SetInt("Level" + (SceneManager.GetActiveScene().
                buildIndex - 1).ToString() + "CurrentStarsCount", currentStarsCount);
    }
}
