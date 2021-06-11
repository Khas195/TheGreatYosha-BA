using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Inventory")]
public class InventoryObject : ScriptableObject
{
    [SerializeField]
    List<ItemObject> container = new List<ItemObject>();

    public bool AddItem(ItemObject newItem)
    {
        if (newItem == null)
        {
            return false;
        }
        if (container.Contains(newItem) == false)
        {
            LogHelper.Log("Add new Item to Inventory: " + newItem);
            container.Add(newItem);
        }
        newItem.IncreaseAmountInInventory();
        LogHelper.Log(newItem + " increases count in Inventory by 1" + "- Current Amount : " + newItem.GetAmountInInventory());
        return true;
    }
    public bool RemoveItem(ItemObject targetItem)
    {
        if (container.Contains(targetItem) == false)
        {
            LogHelper.LogWarning("Trying to remove an item that does not exist in the inventory");
            return false;
        }
        container.Remove(targetItem);
        targetItem.ResetAmountInInventory();
        return true;
    }

    public void ClearContainer()
    {
        for (int i = 0; i < container.Count; i++)
        {
            container[i].ResetAmountInInventory();
        }
        container.Clear();
    }

    public ItemObject LookUpItem(string itemName)
    {
        return container.Find(item => item.name == itemName);
    }
}
