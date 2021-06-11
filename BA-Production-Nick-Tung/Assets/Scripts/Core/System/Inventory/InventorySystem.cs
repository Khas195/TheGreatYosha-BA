using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnItemEvent : UnityEvent<ItemObject>
{
}
public class InventorySystem : SingletonMonobehavior<InventorySystem>
{
    public OnItemEvent onItemCollected = new OnItemEvent();
    [SerializeField]
    InventoryObject inventory = null;

    [SerializeField]
    InventoryUIManager uIManager = null;

    protected override void Awake()
    {
        base.Awake();
        inventory.ClearContainer();
    }

    public void AddItem(ItemObject newItem)
    {
        if (inventory.AddItem(newItem))
        {
            uIManager.AddItem(newItem);
            onItemCollected.Invoke(newItem);
        }
    }

    public bool IsOpen()
    {
        return uIManager.IsOpen();
    }

    public void RemoveItem(ItemObject targetItem)
    {
        if (inventory.RemoveItem(targetItem))
        {
            uIManager.RemoveItem(targetItem);
        }
    }
    public void FocusItem(ItemObject targetItem)
    {
        uIManager.FocusItem(targetItem);
    }
    public ItemObject GetItem(string itemName)
    {
        return inventory.LookUpItem(itemName);
    }
    public void HideUI()
    {
        this.uIManager.HideUI();
    }
    public void ShowUI()
    {
        this.uIManager.ShowUI();
    }
}
