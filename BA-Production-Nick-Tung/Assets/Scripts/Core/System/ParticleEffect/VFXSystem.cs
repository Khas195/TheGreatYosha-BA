using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
/// <summary>
/// Experimental scripts for spawning fire vfxxxx
/// </summary>
public class VFXSystem : SingletonMonobehavior<VFXSystem>
{
    [SerializeField]
    [Required]
    VFXResources resourcesPack = null;
    protected override void Awake()
    {
        base.Awake();
    }
    public void PlayEffect(VFXResources.VFXList vFX, Vector3 position, Quaternion rotation)
    {
        foreach (var item in resourcesPack.resourcesList)
        {
            if (item.tag == vFX)
            {
                if (item.prefabs.Count <= 0)
                {
                    return;
                }
                var chooseRandom = UnityEngine.Random.Range(0, item.prefabs.Count);
                GameObject.Instantiate(item.prefabs[chooseRandom], position, rotation, this.transform);
            }
        }
    }
}
