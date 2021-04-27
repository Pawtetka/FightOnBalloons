using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TopPanelInfo : MonoBehaviour
{
    [SerializeField] TMP_Text balloonsInfo;
    [SerializeField] TMP_Text coinsInfo;
    [SerializeField] TMP_Text redCoinsInfo;

    void Update()
    {
        if (balloonsInfo != null)
        {
            balloonsInfo.text = PlayerPrefs.GetInt("StarsCount").ToString();
        }
        
        if (coinsInfo != null)
        {
            coinsInfo.text = PlayerPrefs.GetInt("Money").ToString();
        }
        
        if (redCoinsInfo != null)
        {
            redCoinsInfo.text = PlayerPrefs.GetInt("DeathCoins").ToString();
        }
        
    }

}
