using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text[] coins;
    private int[] coinsValues;
    [SerializeField] private GameObject stars;
    private int sceneIndex;
    private int levelStars;

    private void Awake()
    {
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play("Victory");
        }
    }

    void Start()
    {
        LoadLevelInfo();
    }

    private void LoadLevelInfo()
    {
        sceneIndex = PlayerPrefs.GetInt("ActiveLevelIndex");
        levelStars = PlayerPrefs.GetInt("Level" + (sceneIndex - 1).
            ToString() + "CurrentStarsCount");
        for (int i = 0; i < levelStars; i++)
        {
            stars.transform.GetChild(i).gameObject.SetActive(true);
        }
        CountCoins();
    }

    private void CountCoins()
    {
        coinsValues = new int[4];
        var bestStarsCount = PlayerPrefs.GetInt("Level" + (sceneIndex - 1).
            ToString() + "BestStarsCount");
        int starsCount = PlayerPrefs.GetInt("StarsCount");
        var starsCoins = levelStars - bestStarsCount;
        if (starsCoins < 0) 
        {
            starsCoins = 0; 
        }
        coinsValues[0] = Convert.ToInt32(100 * starsCoins * Math.Pow(sceneIndex - 1, 0.75));
        coinsValues[1] = PlayerPrefs.GetInt("EarnedMoney");
        coinsValues[2] = coinsValues[0] + coinsValues[1];
        coins[3].text = PlayerPrefs.GetInt("Money").ToString();
        coinsValues[3] = PlayerPrefs.GetInt("Money") + coinsValues[2];
        PlayerPrefs.SetInt("Money", coinsValues[3]);
        if (bestStarsCount < levelStars)
        {
            PlayerPrefs.SetInt("Level" + (sceneIndex - 1).ToString() + "BestStarsCount", levelStars);
            PlayerPrefs.SetInt("StarsCount", starsCount + (levelStars - bestStarsCount));
        }
        StartCoroutine(TypeNumber(0));
    }

    private IEnumerator TypeNumber(int index)
    {
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play("CoinsCount");
        }
        var currentNumber = Convert.ToInt32(coins[index].text);
        var coinsOnAdding = Convert.ToInt32(coinsValues[index] / 30);
        if (coinsOnAdding == 0)
        {
            coinsOnAdding = 1;
        }

        while (currentNumber < coinsValues[index])
        {
            currentNumber += coinsOnAdding;
            coins[index].text = currentNumber.ToString();
            
            yield return null;
        }

        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Stop("CoinsCount");
        }

        if (index < 3)
        {
            StartCoroutine(TypeNumber(index + 1));
        }
        
    }

    public void GoToMap()
    {
        SceneManager.LoadScene("Map");
        StopCoroutines();
    }

    public void LoadNextLevel()
    {
        var activeIndex = PlayerPrefs.GetInt("ActiveLevelIndex");
        PlayerPrefs.SetInt("ActiveLevelIndex", activeIndex + 1);
        StopCoroutines();
    }

    public void StopCoroutines()
    {
        StopAllCoroutines();
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Stop("CoinsCount");
        }
    }
}
