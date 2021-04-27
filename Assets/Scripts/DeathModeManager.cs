using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathModeManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemiesToSpawn;
    private float _currentEnemiesCount = 3f;
    [SerializeField] private GameObject enemies;
    [SerializeField] private GameObject portal; 
    [SerializeField] private List<GameObject> portalParent;
    private bool canCreate = true;
    [SerializeField] private TextMeshProUGUI currentCoins;
    private int currentEnemies;
    [SerializeField] private GameObject bomb;
    private bool _isbombNull;
    public bool inGame = true;
    private string _musicOnBackName;
    [SerializeField] private int deathScore;

    private void Start()
    {
        _isbombNull = bomb == null;
        PlayerPrefs.SetInt("CurrentDeathCoins", 0);
        _musicOnBackName = "DeathMode";
    }

    private void Update()
    {
        if (enemies.transform.childCount == 0 && canCreate)
        {
            canCreate = false;
            EnemiesSpawn();
        }
        
        if (currentEnemies > enemies.transform.childCount)
        {
            currentEnemies = enemies.transform.childCount;
            var i = PlayerPrefs.GetInt("CurrentDeathCoins");
            PlayerPrefs.SetInt("CurrentDeathCoins", i += deathScore);
            currentCoins.text = i.ToString();
        }
        
        BackMusicPlay();
    }
    
    public void BackMusicPlay()
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
    
    private Vector3 RandomPosition(Transform transform)
    {
        var x = UnityEngine.Random.Range(-15, 15);
        var y = UnityEngine.Random.Range(-5, 5);
        var z = transform.position.z;
        return new Vector3(x, y, z);
    }

    private GameObject PortalInst(int posCoef)
    {
        var position = portal.transform.position;
        var newPortal = Instantiate(portal, new Vector3(position.x + posCoef,
            position.y, position.z), portal.transform.rotation, enemies.transform.parent);
        newPortal.SetActive(true);
        portalParent.Add(newPortal);
        return newPortal;
    }

    private void BombInst()
    {
        if (_isbombNull) return;
        for (var i = 0; i < Convert.ToInt32(_currentEnemiesCount/2); i++)
        {
            var newBomb = Instantiate(bomb, RandomPosition(bomb.transform), 
                bomb.transform.rotation, enemies.transform.parent);  
            newBomb.SetActive(true);
        }
    }

    private void PortalsDestroy()
    {
        foreach (var item in portalParent)
        {
            Destroy(item);
        }
        canCreate = true;
    }


    private void EnemiesSpawn()
    {
        for (var i = 0; i < Convert.ToInt32(_currentEnemiesCount); i++)
        {
            StartCoroutine("EnemiesInst", PortalInst(3 * i));
        }

        Invoke("BombInst", 1.5f);
        
        if (_currentEnemiesCount < 8)
        {
            _currentEnemiesCount += 0.5f;
        }
        
        Invoke("PortalsDestroy", 2f);
    }

    private IEnumerator EnemiesInst(GameObject portal)
    {
        yield return new WaitForSeconds(1f);
        Instantiate(enemiesToSpawn[UnityEngine.Random.Range(0, enemiesToSpawn.Count)],
            portal.transform.position, Quaternion.identity, enemies.transform);
        currentEnemies += 1;
    }

    public void NotInGame()
    {
        inGame = false;
        FindObjectOfType<AudioManager>().Stop(_musicOnBackName);
    }

}
