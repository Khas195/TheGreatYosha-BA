using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "VFXs", menuName = "VFX", order = 0)]
public class VFXResources : ScriptableObject
{
    public enum VFXList
    {
        SwordHitEnemy,
        EnemyHitPlayer,
        Heal,
        SwordHitsRock,
        PlayerLand,
        CrabInterval,
        FootFall,
        SwordHitChicken,
        ChickenDeath,
        SwordHitCrab,
        CrabDeath,
        SwordHitSlime,
        SlimeDeath
    }
    [Serializable]
    public struct VFX
    {
        public VFXList tag;
        public List<GameObject> prefabs;
    }
    public List<VFX> resourcesList = new List<VFX>();
}
