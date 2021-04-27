using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDamage;
    public float BulletDamage { get { return bulletDamage; } }
    public string ParentTag { get; set; }
    private void Start()
    {
        rb.velocity = transform.right * bulletSpeed;
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground") || other.CompareTag("Shield"))
        {
            if (FindObjectOfType<AudioManager>() != null)
            {
                FindObjectOfType<AudioManager>().Play("Shield");
            }
            Destroy(gameObject);
        }
    }
}
