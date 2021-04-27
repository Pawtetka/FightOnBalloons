using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPortal : MonoBehaviour
{
    [SerializeField] private TrainingManager manager;
    [SerializeField] private int levelIndex;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerInfo"))
        {
            Invoke("NextLevel", 0.1f);
        }
    }

    private void NextLevel()
    {
        manager.inGame = false;
        manager.NextLevel(levelIndex);
    }

}
