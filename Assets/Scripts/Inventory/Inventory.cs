using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }
    
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    
    public List<Item> checklist;
    public List<Item> items = new List<Item>();

    private void Start()
    {
        InventoryCheck();
    }

    public void InventoryCheck()
    {
        foreach (var item in checklist.Where(item => PlayerPrefs.GetInt(item.name) == 1))
        {
            items.Add(item);
        }
    }

    public void Add(Item item)
    {
        items.Add(item);
        PlayerPrefs.SetInt(item.name, 1);
        onItemChangedCallback?.Invoke();
    }
    
    public void Remove (Item item)
    {
        items.Remove(item);		// Remove item from list

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public void RemoveAllItemsFromInventory()
    {
        foreach (var item in items)
        {
            PlayerPrefs.SetInt(item.name, 0);
        }
    }
}
