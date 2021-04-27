using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firePosition;
    [SerializeField] private Equipment weapon;
    private bool _canShoot = false;
    private static readonly int Shoot1 = Animator.StringToHash("Shoot");
    public Animator anim;

    private void Start()
    {
        if (gameObject.activeSelf)
        {
            _canShoot = true;
        }
    }

    public void Shoot()
    {
        if (_canShoot && weapon!=null)
        {
            if (FindObjectOfType<AudioManager>())
            {
                FindObjectOfType<AudioManager>().Play(weapon.name);
            }
            var bull = Instantiate(weapon.bullet, firePosition.position, firePosition.transform.rotation);
            bull.GetComponent<Bullet>().ParentTag = gameObject.transform.parent.tag;
            _canShoot = false;
            anim.SetTrigger(Shoot1);
            Invoke("EndShoot", 0.15f);
            Invoke("CanShoot", weapon.fireRate);
        }
    }

    private void CanShoot()
    {
        _canShoot = true;
    }
    private void EndShoot()
    {
        anim.SetTrigger("BackToIdle");
    }
}
