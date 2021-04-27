using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Information : MonoBehaviour
{
    private Button _button;
    [SerializeField] private int levelNumber;
    private int _currentLevelQuantity;
    [SerializeField] private GameObject stars;
    private int levelStars;

    private void Awake()
    {
        _currentLevelQuantity = PlayerPrefs.GetInt("LevelQuantity");
    }

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.interactable = _currentLevelQuantity >= levelNumber;
        levelStars = PlayerPrefs.GetInt("Level" + (levelNumber).
            ToString() + "BestStarsCount");
        for (int i = 0; i < levelStars; i++)
        {
            if (stars != null)
            {
                stars.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
