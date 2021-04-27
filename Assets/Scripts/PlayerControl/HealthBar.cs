using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private PlayerInfo playerHealth;

    void FixedUpdate() 
    {
        UpdateHP();
    }

    void UpdateHP()
    {
        slider.value = playerHealth.Health;
        if (playerHealth.Health <= 0)
        {
            slider.fillRect.gameObject.SetActive(false);
        }
        else
        {
            slider.fillRect.gameObject.SetActive(true);
        }
    }
}
