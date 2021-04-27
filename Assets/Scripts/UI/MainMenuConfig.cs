using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuConfig : MonoBehaviour
    {   
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _settingsMenu;
        private EquipmentSlot _equipmentSlot;
        [SerializeField] private GameObject newGameWarning;

        private void Awake()
        {
            _mainMenu.SetActive(true);
            _settingsMenu.SetActive(false);
            //PlayerPrefs.SetInt("LevelQuantity", 22);
            /*PlayerPrefs.SetInt("Money", 0);
            PlayerPrefs.SetInt("Level", 0);
            PlayerPrefs.SetInt("Bat", 0);
            PlayerPrefs.SetInt("Owl", 0);
            PlayerPrefs.SetInt("417", 0);
            PlayerPrefs.SetInt("AWP", 0);
            PlayerPrefs.SetInt("Famas", 0);
            PlayerPrefs.SetInt("P90", 0);
            PlayerPrefs.SetInt("Six12SD", 0);
            PlayerPrefs.SetInt("Raven", 0);*/
        }

        public void LoadLevel()
        {
            if(PlayerPrefs.GetInt("LevelQuantity") > 0)
            {
                SceneManager.LoadScene("Map");
            }
            else
            {
                PlayerPrefs.SetInt("Money", 0);
                PlayerPrefs.SetInt("LevelQuantity", 0);
                PlayerPrefs.SetInt("Level", 0);
                PlayerPrefs.SetInt("ActiveLevelIndex", (20));
                SceneManager.LoadScene("Training1");
            }
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
