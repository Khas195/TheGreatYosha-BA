using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{

    [SerializeField]
    GameObject hostObject = null;
    [SerializeField]
    GameObject slotPrefab = null;
    [SerializeField]
    Transform viewPoint = null;
    [SerializeField]
    GameObject contentBox = null;
    [SerializeField]
    List<InventorySlot> slots = new List<InventorySlot>();
    InventorySlot activeSlot = null;
    [SerializeField]
    Text description = null;

    public void AddItem(ItemObject newItem)
    {
        var newSlotObject = GameObject.Instantiate(slotPrefab, contentBox.transform);
        newSlotObject.transform.localPosition = Vector3.zero;
        newSlotObject.transform.localRotation = Quaternion.identity;

        var newViewObject = GameObject.Instantiate(newItem.prefab, viewPoint);
        newViewObject.transform.localPosition = Vector3.zero;
        newViewObject.transform.localRotation = Quaternion.identity;
        newViewObject.SetActive(false);

        var slot = newSlotObject.GetComponent<InventorySlot>();
        slot.SetViewItem(newViewObject);
        slot.SetItemObject(newItem);
        slots.Add(slot);


        var button = newSlotObject.GetComponent<Button>();
        button.onClick.AddListener(() => OnItemClicked(slot));

        if (activeSlot == null)
        {
            FocusItem(slot.GetItemObject());
        }
    }

    public void ShowUI()
    {
        hostObject.SetActive(true);
    }

    public void HideUI()
    {
        hostObject.SetActive(false);
    }

    public bool IsOpen()
    {
        return hostObject.activeSelf;
    }

    public void RemoveItem(ItemObject targetItem)
    {
        var targetSlot = slots.Find(slot => slot.GetItemObject() == targetItem);

        if (targetSlot == null)
        {
            LogHelper.Log("Trying to remove an item from the Ui where it doesn't exist");
            return;
        }

        slots.Remove(targetSlot);

        if (activeSlot == targetSlot)
        {
            if (TrySelectNextItem(targetSlot))
            {
                activeSlot = null;
            }
        }

        Destroy(targetSlot.GetViewObject());
        Destroy(targetSlot.gameObject);
    }

    private bool TrySelectNextItem(InventorySlot targetSlot)
    {
        if (slots.Count > 2)
        {
            var targetSlotIndex = slots.IndexOf(targetSlot);
            if (targetSlotIndex + 1 >= slots.Count)
            {
                targetSlotIndex = 0;
            }
            FocusItem(slots[targetSlotIndex].GetItemObject());
            return true;
        }
        return false;
    }

    public void FocusItem(ItemObject targetItem)
    {
        var targetSlot = slots.Find(slot => slot.GetItemObject() == targetItem);
        if (targetSlot == null)
        {
            LogHelper.Log("Trying to focus an item that is not in the inventory ui list");
            return;
        }
        if (activeSlot)
        {
            activeSlot.GetViewObject().SetActive(false);
        }
        description.text = targetSlot.GetItemObject().description;
        targetSlot.GetViewObject().SetActive(true);
        activeSlot = targetSlot;
    }
    public void OnItemClicked(InventorySlot slotClicked)
    {
        FocusItem(slotClicked.GetItemObject());
    }
}
