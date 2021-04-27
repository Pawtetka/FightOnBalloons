using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    public List<Image> equipmentImages;
    public static EquipmentManager instance;
    private EquipmentSlot _equipmentSlot;
    [SerializeField] private List<Equipment> checkList;

    private void Awake ()
    { 
        instance = this;
    }

    public Equipment[] currentEquipment; 
    
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;
    
    Inventory inventory;

    void Start ()
    {
        inventory = Inventory.instance;
        
        var numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = EquipmentCheck(numSlots);
    }

    public Equipment[] EquipmentCheck(int numSlots)
    {
        var equipments = new Equipment[numSlots];
        
        foreach (var item in checkList)
        {
            switch (item.equipSlot)
            {
                case EquipmentSlot.Weapon when item.name == PlayerPrefs.GetString("Active" + item.equipSlot):
                    equipments[0] = item;
                    equipmentImages[0].sprite = item.icon;
                    equipmentImages[0].enabled = true;
                    inventory.checklist.Remove(item);
                    break;
                case EquipmentSlot.Companion when item.name == PlayerPrefs.GetString("Active" + item.equipSlot):
                    equipments[1] = item;
                    equipmentImages[1].sprite = item.icon;
                    equipmentImages[1].enabled = true;
                    inventory.checklist.Remove(item);
                    break;
            }
        }

        return equipments;
    }

    public void Equip (Equipment newItem)
    {
        // Find out what slot the item fits in
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = Unequip(slotIndex);

        // An item has been equipped so we trigger the callback
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        // Insert the item into the slot
        currentEquipment[slotIndex] = newItem;
        equipmentImages[slotIndex].sprite = newItem.icon;
        equipmentImages[slotIndex].enabled = true;
        PlayerPrefs.SetString("Active" + newItem.equipSlot, newItem.name);
        inventory.checklist.Remove(newItem);
    }
    
    public Equipment Unequip (int slotIndex)
    {
        Equipment oldItem = null;
        // Only do this if an item is there
        if (currentEquipment[slotIndex] != null)
        {
            // Add the item to the inventory
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            // Remove the item from the equipment array
            currentEquipment[slotIndex] = null;
            equipmentImages[slotIndex].sprite = null;
            equipmentImages[slotIndex].enabled = false;

            // Equipment has been removed so we trigger the callback
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
            
            PlayerPrefs.SetString("Active" + oldItem.equipSlot, null);
            inventory.checklist.Add(oldItem);
        }
        
        
        return oldItem;
    }
    
    public void UneEquipAll ()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }
}
