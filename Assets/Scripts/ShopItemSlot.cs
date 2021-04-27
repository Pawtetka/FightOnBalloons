using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour
{
    [SerializeField] private int needStars;
    [SerializeField] private Item item;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private TMP_Text purchaseText;
    [SerializeField] private GameObject lockedBackground;
    [SerializeField] private Button changeHolderButton;
    [SerializeField] private List<GameObject> infoPanels;
    private int _starsCount;
    private bool _isPurchased;
    private bool _isCurrentHolderActive;

    private void Start()
    {
        _isCurrentHolderActive = true;
        _starsCount = PlayerPrefs.GetInt("StarsCount");
        if (!_isPurchased)
        {
            if (_starsCount >= needStars)
            {
                ActiveSlot();
            }
            else if (needStars >= 0)
            {
                LockedSlot();
            }
            
            if (needStars < 0)
            {
                SpecialSlot();
            }
        }
    }

    private void Update()
    {
        _isPurchased = PlayerPrefs.GetInt(item.name) == 1;
        if (_isPurchased)
        {
            PurchasedSlot();
        }
    }

    private void PurchasedSlot()
    {
        lockedBackground.SetActive(false);
        purchaseButton.interactable = false;
        purchaseText.text = "Purchased";
    }
    
    private void ActiveSlot()
    {
        lockedBackground.SetActive(false);
        purchaseButton.interactable = true;
        purchaseText.text = $"{item.price} coins";
    }

    private void LockedSlot()
    {
        lockedBackground.SetActive(true);
        purchaseButton.interactable = false;
        purchaseText.text = $"{needStars} balloons";
    }

    private void SpecialSlot()
    {
        lockedBackground.SetActive(true);
        purchaseButton.interactable = false;
        purchaseText.text = "Special item";
    }
}
