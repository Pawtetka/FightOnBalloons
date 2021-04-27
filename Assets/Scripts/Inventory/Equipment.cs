using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

    public EquipmentSlot equipSlot;

    public int bulletsQuantity;
    public float fireRate;
    public GameObject bullet;
    public int health;
    public GameObject prefab;
    

    public override void Use()
    {
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }
}

public enum EquipmentSlot {Weapon, Companion}


