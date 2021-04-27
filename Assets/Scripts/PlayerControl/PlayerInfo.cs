using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour
{
    public float Health { get; set; }
    public int Money { get; set; }
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private DeathModeManager _deathModeManager;
    [SerializeField] private TrainingManager _trainingManager;
    public GameObject obj;
    public Animator _animator;
    private static readonly int Death = Animator.StringToHash("Death");
    private bool isAlive = true;
    [SerializeField] private GameObject blood;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject balloons;
    private bool isDeathModeNull;
    private bool isTrainingModeNull;

    private void Start()
    {
        Health = 100f;
        if (_deathModeManager == null)
        {
            isDeathModeNull = true;
        }
        if (_trainingManager == null)
        {
            isTrainingModeNull = true;
        }
    }

    private void Update()
    {
        if (!(Health <= 0)) return;
        if (!isAlive) return;
        if (FindObjectOfType<AudioManager>() != null)
        {
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
        }
        DeathAnim();
        DestroyItems();
        
        if (levelManager != null)
        {
            PlayerPrefs.SetInt("EarnedMoney", levelManager.Coins);
            levelManager.inGame = false;
        }
        
        if (!isDeathModeNull)
        {
            _deathModeManager.inGame = false;
        }

        if (!isTrainingModeNull)
        {
            _trainingManager.inGame = false;
        }
        Invoke(nameof(SceneReload), 0.75f);
    }

    private void DestroyItems()
    {
        var balloons = gameObject.transform.parent.GetChild(3);
        for (int i = 0; i < balloons.childCount; i++)
        {
            balloons.GetChild(i).GetComponent<BalloonInfo>().health = 0;
        }
        gameObject.transform.parent.GetChild(4).gameObject.SetActive(false);
    }
    private void SceneReload()
    {
        Destroy(obj);
        SceneManager.LoadScene("DieScene");
    }

    private void DeathAnim()
    {
        _animator.SetTrigger(Death);
        isAlive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") &&
            !gameObject.transform.parent.CompareTag(other.GetComponent<Bullet>().ParentTag))
        {
            if (FindObjectOfType<AudioManager>() != null)
            {
                FindObjectOfType<AudioManager>().Play("DamageTake");
            }
            StartBlood();
            Invoke("EndBlood", 0.35f);
            Health -= other.gameObject.GetComponent<Bullet>().BulletDamage;
            Destroy(other.gameObject, 0.01f);
        }
        else if (other.CompareTag("Coin"))
        {
            if (FindObjectOfType<AudioManager>() != null)
            {
                FindObjectOfType<AudioManager>().Play("CoinPick");
            }
            levelManager.Coins += 10;
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("BigCast"))
        {
            if (FindObjectOfType<AudioManager>() != null)
            {
                FindObjectOfType<AudioManager>().Play("DamageTake");
            }
            StartBlood();
            Invoke("EndBlood", 0.35f);
            Health = 0;
        }
        
        if (other.gameObject.tag == "DeathZone")
        {
            Health = 0;
        }
        
        if (other.CompareTag("Bomb"))
        {
            if (FindObjectOfType<AudioManager>() != null)
            {
                FindObjectOfType<AudioManager>().Play("DamageTake");
            }
            Health = 0f;
            other.GetComponent<Animator>().SetTrigger("Explode");
            if (FindObjectOfType<AudioManager>() != null)
            {
                FindObjectOfType<AudioManager>().Play("Explosion");
            }
            Destroy(other.gameObject, 0.41f);
        }
    }

    public void EndBlood()
    {
        blood.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("PermanentCast"))
        {
            blood.SetActive(true);
            Health -= 1;
        }

        if (other.CompareTag("WaterFall"))
        {
            rb.AddForce(new Vector2(0, -375f * Time.deltaTime));
        }
        
        if (other.CompareTag("River"))
        {
            rb.AddForce(new Vector2(375f * Time.deltaTime, 0));
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PermanentCast"))
        {
            EndBlood();
        }
    }


    public void StartBlood()
    {
        blood.SetActive(true);
    }
}