using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private BossController bossHealth;

    void FixedUpdate()
    {
        UpdateHP();
    }

    void UpdateHP()
    {
        slider.value = bossHealth.health;
        if (bossHealth.health <= 0)
        {
            slider.fillRect.gameObject.SetActive(false);
        }
    }

}
