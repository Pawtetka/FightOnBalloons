using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChanger : MonoBehaviour
{
    //private Equipment[] weapons;
    [SerializeField] private GameObject playerWeapons;
    public Animator anim;
    private bool canShoot = false;
    

    private void Start()
    {
        Invoke("CanShoot", 1f);
    }

    public void Fire()
    {
        if (canShoot && playerWeapons.gameObject.GetComponent<WeaponsController>() != null)
        {
            playerWeapons.gameObject.GetComponent<WeaponsController>().Shoot();
        }
    }

    void CanShoot()
    {
        canShoot = true;
    }
}
