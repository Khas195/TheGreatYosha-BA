using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;


[CreateAssetMenu(fileName = "DefaultItem", menuName = "Inventory/DefaultItem")]
public class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public Sprite portrait;

    [TextArea(15, 20)]
    public string description;

    [SerializeField]
    [ReadOnly]
    private int amountInInventory = 0;
    public void ResetAmountInInventory() {
        amountInInventory = 0;
    }
    public void IncreaseAmountInInventory(){
        amountInInventory ++;
    }
    public void DecreaseAmountInInventory(){
        amountInInventory--;
    }

    public int GetAmountInInventory()
    {
        return amountInInventory;
    }
}
