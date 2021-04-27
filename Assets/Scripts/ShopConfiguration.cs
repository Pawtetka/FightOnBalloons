using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopConfiguration : MonoBehaviour
{
    [SerializeField] private GameObject notEnoughMoney;
    private int _currentMoney;
    private Inventory _inventory;

    private void Start()
    {
        _inventory = Inventory.instance;
        
        //_inventory.onItemChangedCallback += _inventory.InventoryCheck;
    }
    
    public void GoBackToMapMenu()
    {
        SceneManager.LoadScene("Map");
    }

    public void Purchase(Item item)
    {
        _currentMoney = PlayerPrefs.GetInt(item.moneyType);
        if (_currentMoney < item.price && _inventory.items.All(inventoryItem => item != inventoryItem))
        {
            notEnoughMoney.SetActive(true);
            return;
        }

        if (_inventory.items.Any(inventoryItem => item == inventoryItem))
        {
            return;
        }
        
        _inventory.Add(item);
        PlayerPrefs.SetInt(item.moneyType, _currentMoney -= item.price);
    }
}
