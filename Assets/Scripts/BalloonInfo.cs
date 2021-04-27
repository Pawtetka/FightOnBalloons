using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonInfo : MonoBehaviour
{
    private int _startHealth;
    public int health;
    private Animator _animator;
    private bool _notPoped = true;
    private static readonly int Pop1 = Animator.StringToHash("Pop");

    private void Start()
    {
        _startHealth = health;
        _animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (health <= 0 && _notPoped)
        {
            _animator.SetTrigger(Pop1);
            _notPoped = false;
            Invoke("Pop", 0.15f);
            health = _startHealth;
            if (FindObjectOfType<AudioManager>() != null)
            {
                FindObjectOfType<AudioManager>().Play("BalloonPop");
            }
        }
    }

    private void Pop()
    {
        gameObject.SetActive(false);
        _notPoped = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.collider.CompareTag($"PlayerLegs") && 
             !gameObject.transform.parent.CompareTag("PlayerBalloons")) ||
            ( other.collider.CompareTag($"EnemyLegs") && 
              !gameObject.transform.parent.CompareTag("EnemyBalloons")) ||
            other.collider.CompareTag($"Spikes") && 
            !gameObject.transform.parent.CompareTag("EnemyBalloons"))
        {
            health--;
        }
        else if (other.collider.CompareTag("Companion") && 
                 !gameObject.transform.parent.CompareTag("PlayerBalloons"))
        {
            health--;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") && !gameObject.transform.parent.
            parent.CompareTag(other.GetComponent<Bullet>().ParentTag))
        {
            var damage = Convert.ToInt32(other.gameObject.GetComponent<Bullet>().BulletDamage);
            health -= damage;
            Destroy(other.gameObject, 0.01f);
        }
        if (other.CompareTag("IceShard"))
        {
            health = 0;
        }
    }
}
