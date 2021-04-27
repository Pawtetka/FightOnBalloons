using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CompanionEnum {Active, HalfActive, Passive}

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Companion")]
public class Companion : Equipment
{
    public CompanionEnum companionEnum;
    public override void Use()
    {
        base.Use();
    }
}
