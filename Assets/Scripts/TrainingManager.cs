using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrainingManager : MonoBehaviour
{
    [SerializeField] EnemyMain enemies;
    [SerializeField] EnemyMain secondEnemies;

    [SerializeField] List<DialogueTrigger> trigger;
    [SerializeField] GameObject adventurer;
    [SerializeField] float invokeTime;
    [SerializeField] GameObject ravenKing;
    private bool canDialog = true;
    public bool inGame = true;
    private string _musicOnBackName;

    private void Start()
    {
        Invoke("StartAdventurer", 0.5f);
        Invoke("InvokeDialog", invokeTime);
        _musicOnBackName = "TrainingMusic";
    }

    private void Update()
    {
        if (enemies != null && enemies.AliveCount() == 0)
        {
            if (secondEnemies != null && secondEnemies.AliveCount() == 0 && canDialog)
            {
                trigger[1].TriggerDialogue();
                canDialog = false;
            }
            else if (!secondEnemies.gameObject.activeSelf && !ravenKing.activeSelf)
            {
                ravenKing.SetActive(true);
            }
        }
        BackMusicPlay();
    }
    
    private void BackMusicPlay()
    {
        if (FindObjectOfType<AudioManager>() != null)
        {
            if (!inGame)
            {
                FindObjectOfType<AudioManager>().Stop(_musicOnBackName);
                return;
            }
            if (!FindObjectOfType<AudioManager>().FindAudioSource(_musicOnBackName).source.isPlaying)
            {
                FindObjectOfType<AudioManager>().Play(_musicOnBackName);
            }
            else if (FindObjectOfType<AudioManager>().FindAudioSource(_musicOnBackName).source.isPlaying)
            {
                return;
            }
        }
    }

    private void StartAdventurer()
    {
        if (adventurer != null)
        {
            adventurer.SetActive(true);
        }
    }

    private void InvokeDialog()
    {
        trigger[0].TriggerDialogue();
    }

    public void NextLevel(int index)
    {
        PlayerPrefs.SetInt("ActiveLevelIndex", (index + 26));
        if (index > 3)
        {
            NotInGame();
            PlayerPrefs.SetInt("LevelQuantity", 1);
            PlayerPrefs.SetInt("Level", 0);
            SceneManager.LoadScene("Map");
        }
        else
        {
            SceneManager.LoadScene("Training" + index.ToString());
        }
    }
    public void NotInGame()
    {
        inGame = false;
        FindObjectOfType<AudioManager>().Stop(_musicOnBackName);
    }

}
