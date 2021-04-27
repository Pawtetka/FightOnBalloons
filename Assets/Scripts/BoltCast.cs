using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltCast : MonoBehaviour
{
    private Animator _animator;
    private static readonly int Contact = Animator.StringToHash("Contact");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent == null) return;
        
        if (((other.CompareTag("PlayerInfo") 
            || other.transform.parent.CompareTag("Player")) && !other.transform.parent.CompareTag("Enemies")) 
            || other.CompareTag("Ground"))
        {
            _animator.SetTrigger(Contact);
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            if (FindObjectOfType<AudioManager>() != null)
            {
                FindObjectOfType<AudioManager>().Play("IceSplash");
            }
            Destroy(gameObject, 0.25f);
        }
    }
}
