using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public new string name;
    public int price;
    public string typeOfItem;
    public Sprite icon;
    public string moneyType;

    public virtual void Use()
    {
        
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
    
}
