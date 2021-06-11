using System;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    Image portrait = null;
    GameObject viewItemPrefab = null;
    ItemObject item = null;

    public ItemObject GetItemObject()
    {
        return item;
    }

    public void SetItemObject(ItemObject newItem)
    {
        this.item = newItem;
        portrait.sprite = newItem.portrait;
    }

    public void SetViewItem(GameObject newViewObject)
    {
        this.viewItemPrefab = newViewObject;
    }

    public GameObject GetViewObject()
    {
        return this.viewItemPrefab;
    }
    
}
