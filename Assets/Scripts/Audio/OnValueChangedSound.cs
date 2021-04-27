using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OnValueChangedSound : MonoBehaviour
{
    // [Header("for sliders, buttons")]
    // private Slider slider;
    // private Button button;
    [SerializeField] private string soundName;

    void Start()
    {
        if(TryGetComponent(out Button button))
        {
            button.onClick.AddListener(PlaySound);
        }
        /*
        if(TryGetComponent(out Slider slider))
        {
            slider.onValueChanged.AddListener(delegate {
                PlaySound();
            });
        }
        */
    }

    void PlaySound()
    {
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play(soundName);
        }
    }
}
