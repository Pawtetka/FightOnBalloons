using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;

    void Awake()
    {
        Restrictor();
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.playOnAwake = false;
            s.source.clip = s.Clip;
            s.source.outputAudioMixerGroup = s.mixerGroup;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    
    void Update()
    {
        MainThemeController();
    }

    void Restrictor()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public Sound FindAudioSource(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s;
    }

    public void Play(string name)
    {
        if (FindAudioSource(name) == null)
        {
            return;
        }
        //FindAudioSource(name).source.PlayOneShot(FindAudioSource(name).Clip);
        FindAudioSource(name).source.Play();
    }

    public void Stop(string name)
    {
        FindAudioSource(name).source.Stop();
    }

    private void MainThemeController()
    {
        if ((SceneManager.GetActiveScene().name == "MainMenu" ||
        SceneManager.GetActiveScene().name == "Map" ||
        SceneManager.GetActiveScene().name == "PlayerPreparation" ||
        SceneManager.GetActiveScene().name == "Shop") && 
        !FindAudioSource("MainTheme").source.isPlaying)
        {
            FindAudioSource("MainTheme").source.Play();
        }

        if ((SceneManager.GetActiveScene().name == "MainMenu" ||
        SceneManager.GetActiveScene().name == "Map" ||
        SceneManager.GetActiveScene().name == "PlayerPreparation" ||
        SceneManager.GetActiveScene().name == "Shop") && 
        FindAudioSource("MainTheme").source.isPlaying)
        {
            return;
        }

        if (SceneManager.GetActiveScene().name != "MainMenu" ||
        SceneManager.GetActiveScene().name != "Map" ||
        SceneManager.GetActiveScene().name != "PlayerPreparation" ||
        SceneManager.GetActiveScene().name != "Shop")
        {
            FindAudioSource("MainTheme").source.Stop();
        }
    }
}
