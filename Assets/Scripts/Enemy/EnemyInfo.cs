using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField] private float health;
    public float Health { get { return health; } set { health = value; } }
    [SerializeField] private EnemyAI AI;
    private bool isAlive = true;
    [SerializeField] private float touchDamage;
    public Animator animator;
    private static readonly int Death1 = Animator.StringToHash("Death");
    private SpriteRenderer _sprite;
    private Color32 _startColor;
    [SerializeField] private GameObject balloons;
    private bool _isBalloons;
    [SerializeField] private string dieAudio;

    private void Start()
    {
        _sprite = gameObject.transform.parent.GetComponent<SpriteRenderer>();
        _startColor = _sprite.color;
        _isBalloons = balloons != null;
    }

    private void Update()
    {
        if (!(health <= 0)) return;
        if (!isAlive) return;
        animator.SetTrigger(Death1);
        if (FindObjectOfType<AudioManager>() != null && dieAudio != null)
        {
            FindObjectOfType<AudioManager>().Play(dieAudio);
        }
        isAlive = false;
        AI.state = EnemyAI.State.Death;
        if (_isBalloons)
        {
            balloons.SetActive(false);
        }
        Invoke("Death", 0.75f);
    }

    public void StartBlood()
    {
        _sprite.color = Color.red;
    }

    public void EndBlood()
    {
        _sprite.color = _startColor;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Bullet" && other.GetComponent<Bullet>().ParentTag 
            != gameObject.transform.parent.tag)
        {
            health -= other.gameObject.GetComponent<Bullet>().BulletDamage;
            StartBlood();
            Invoke("EndBlood", 0.15f);
            Destroy(other.gameObject, 0.01f);
        }

        if (other.gameObject.tag == "DeathZone")
        {
            Health = 0;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Player"))
        {
            if (AI.state == EnemyAI.State.Inflating && isAlive)
            {
                health -= 2000f;
                /*animator.SetTrigger(Death1);
                isAlive = false;
                AI.state = EnemyAI.State.Death;
                if (_isBalloons)
                {
                    balloons.SetActive(false);
                }
                Invoke("Death", 0.75f);*/
            }
            else
            {
                var playerInfo = GameObject.FindGameObjectWithTag("PlayerInfo").GetComponent<PlayerInfo>();
                playerInfo.Health -= touchDamage;
                playerInfo.StartBlood();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if ((other.gameObject.tag == "Player"))
        {
            var playerInfo = GameObject.FindGameObjectWithTag("PlayerInfo").GetComponent<PlayerInfo>();
            playerInfo.EndBlood();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Player"))
        {
            if (AI.state == EnemyAI.State.Inflating && isAlive)
            {
                health -= 2000;
                /*isAlive = false;
                AI.state = EnemyAI.State.Death;
                if (_isBalloons)
                {
                    balloons.SetActive(false);
                }
                Invoke("Death", 0.75f);*/
            }
        }
        else if (collision.gameObject.CompareTag("Companion"))
        {
            if (AI.state == EnemyAI.State.Inflating && isAlive)
            {
                animator.SetTrigger(Death1);
                isAlive = false;
                AI.state = EnemyAI.State.Death;
                if (_isBalloons)
                {
                    balloons.SetActive(false);
                }
                Invoke("Death", 0.75f);
            }
            else
            {
                StartBlood();
                Invoke("EndBlood", 0.15f);
                health -= collision.gameObject.GetComponent<CompanionAI>().Damage;
            }
        }
    }
    private void Death()
    {
        Destroy(gameObject.transform.parent.gameObject);
        isAlive = false;
    }

}
