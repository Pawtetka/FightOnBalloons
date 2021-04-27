using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [SerializeField] private List<Equipment> weapons;
    private EquipmentManager _equipmentManager;
    private Weapon _activeWeapon;
    void Start()
    {
        try
        {
            _equipmentManager = EquipmentManager.instance;
            CreateWeapon();
        }
        catch { }
    }


    public void Shoot()
    {
        if (_activeWeapon != null)
        {
            _activeWeapon.Shoot();
        }
    }
    
    private void CreateWeapon()
    {
        var weapon = _equipmentManager.currentEquipment[0];
        if (weapon != null)
        {
            foreach (Equipment weap in weapons)
            {
                if (weap.name == weapon.name)
                {
                    var activeWeapon = Instantiate(weap.prefab, this.transform);
                    _activeWeapon = activeWeapon.GetComponent<Weapon>();
                    return;
                }
            }
        }
    }
}
