using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMenuConfig : MonoBehaviour
{
    public void GotoShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void GotoPlayerLevelUp(int sceneIndex)
    {
        PlayerPrefs.SetInt("ActiveLevelIndex", sceneIndex);
        SceneManager.LoadScene("PlayerPreparation");
    }
    
    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GetAll()
    {
        PlayerPrefs.SetInt("LevelQuantity", 22);
        PlayerPrefs.SetInt("Bat", 1);
        PlayerPrefs.SetInt("Owl", 1);
        PlayerPrefs.SetInt("417", 1);
        PlayerPrefs.SetInt("AWP", 1);
        PlayerPrefs.SetInt("Famas", 1);
        PlayerPrefs.SetInt("P90", 1);
        PlayerPrefs.SetInt("Six12SD", 1);
        PlayerPrefs.SetInt("Raven", 1);
    }
}
