using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;		
    Item _item;  
    
    public void AddItem (Item newItem)
    {
        _item = newItem;

        icon.sprite = _item.icon;
        icon.enabled = true;
    }
    
    public void ClearSlot ()
    {
        _item = null;

        icon.sprite = null;
        icon.enabled = false;
    }
    
    public void UseItem ()
    {
        if (_item != null)
        {
            _item.Use();
        }
    }
}
