using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Item : IInteractable
{
    [SerializeField]
    GameObject itemEntity = null;
    ItemObject itemData = null;
    [SerializeField]
    bool useInventoryUI = false;
    [SerializeField]
    [ShowIf("useInventoryUI")]
    Canvas itemCanvas = null;
    void Awake()
    {
    }
    public override void Defocus()
    {
        base.Defocus();
        if (useInventoryUI)
        {
            itemCanvas.gameObject.SetActive(false);
        }
    }

    public override void Focus()
    {
        base.Focus();
        if (useInventoryUI)
        {
            itemCanvas.gameObject.SetActive(true);
        }
    }

    public override string GetKindOfInteraction()
    {
        return "Pick Up";
    }


    public override bool Interact()
    {
        if (base.Interact() == false) return false;
        InventorySystem.GetInstance().AddItem(this.itemData);
        Destroy(this.itemEntity);
        return true;
    }


}

