using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [SerializeField] Animator _anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.transform.parent.CompareTag("Player"))
        {
            _anim.SetTrigger("Explode");
            Destroy(gameObject, 0.41f);
        }
    }
}
