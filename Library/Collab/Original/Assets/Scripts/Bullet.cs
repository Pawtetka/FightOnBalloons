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
    
    void Start()
    {
        rb.velocity = transform.right * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
