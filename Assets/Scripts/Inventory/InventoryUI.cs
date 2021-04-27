using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;

    Inventory inventory;	// Our current inventory

    InventorySlot[] slots;	// List of all the slots

    void Start () 
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUi;
        
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        UpdateUi();
    }

    void UpdateUi ()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)	
            {
                slots[i].AddItem(inventory.items[i]);	
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
