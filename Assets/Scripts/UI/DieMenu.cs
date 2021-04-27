using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DieMenu : MonoBehaviour
{
    [SerializeField] private List<GameObject> _list;
    [SerializeField] private Image coinImage;
    [SerializeField] private Sprite[] images;
    [SerializeField] private GameObject goToMapButton;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("ActiveLevelIndex") <= SceneManager.sceneCountInBuildSettings - 6
            || PlayerPrefs.GetInt("ActiveLevelIndex") >= SceneManager.sceneCountInBuildSettings - 3)
        {
            goToMapButton.SetActive(true);
        }
        if (PlayerPrefs.GetInt("ActiveLevelIndex") >= SceneManager.sceneCountInBuildSettings - 3
            && PlayerPrefs.GetInt("ActiveLevelIndex") <= SceneManager.sceneCountInBuildSettings - 1)
        {
            foreach (var item in _list)
            {
                item.SetActive(true);
            }
            coinImage.sprite = images[1];
            CountDeathCoins();
            _list[2].GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("Money").ToString();
        }
        else
        {
            coinImage.sprite = images[0];
            CountCoins();
            _list[1].GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("DeathCoins").ToString();
        }

        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play("Lose");
        }
    }

    private void CountDeathCoins()
    {
        var coinsValues = new int[2];
        var coinsTexts = new TMP_Text[2];
        coinsValues[0] = PlayerPrefs.GetInt("CurrentDeathCoins");
        coinsValues[1] = PlayerPrefs.GetInt("DeathCoins") + coinsValues[0];
        coinsTexts[0] = _list[0].GetComponent<TMP_Text>();
        coinsTexts[1] = _list[1].GetComponent<TMP_Text>();
        coinsTexts[1].text = PlayerPrefs.GetInt("DeathCoins").ToString();
        PlayerPrefs.SetInt("DeathCoins", coinsValues[1]);
        StartCoroutine(TypeNumber(0, coinsValues, coinsTexts));
    }

    private void CountCoins()
    {
        var coinsValues = new int[2];
        var coinsTexts = new TMP_Text[2];
        coinsValues[0] = PlayerPrefs.GetInt("EarnedMoney");
        coinsValues[1] = PlayerPrefs.GetInt("Money") + coinsValues[0];
        coinsTexts[0] = _list[0].GetComponent<TMP_Text>();
        coinsTexts[1] = _list[2].GetComponent<TMP_Text>();
        coinsTexts[1].text = PlayerPrefs.GetInt("Money").ToString();
        PlayerPrefs.SetInt("Money", coinsValues[1]);
        StartCoroutine(TypeNumber(0, coinsValues, coinsTexts));
    }

    private IEnumerator TypeNumber(int index, int[] mass, TMP_Text[] texts)
    {
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play("CoinsCount");
        }
        int currentNumber = Convert.ToInt32(texts[index].text);
        while (currentNumber < mass[index])
        {
            currentNumber += 1;
            texts[index].text = currentNumber.ToString();
            yield return new WaitForSeconds(0.1f);
        }

        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Stop("CoinsCount");
        }

        if (index < 1)
        {
            StartCoroutine(TypeNumber(index + 1, mass, texts));
        }
    }

    public void GoToMap()
    {
        SceneManager.LoadScene("Map");
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
