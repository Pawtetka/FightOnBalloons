using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    [Header("Sliders Info")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private FloatValue musicSliderValue;
    [SerializeField] private Slider effectsSlider;
    [SerializeField] private FloatValue effectsSliderValue;

    void Start()
    {
        musicSlider.value = musicSliderValue.RuntimeValue;
        effectsSlider.value = effectsSliderValue.RuntimeValue;
    }

    public void SetEffectsVolume(float volume)
    {
        audioMixer.SetFloat("effectsVolume", Mathf.Log10(volume) * 20);
        effectsSliderValue.RuntimeValue = effectsSlider.value;
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
        musicSliderValue.RuntimeValue = musicSlider.value;
    }
}
