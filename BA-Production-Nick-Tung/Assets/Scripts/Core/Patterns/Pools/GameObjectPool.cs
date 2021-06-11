using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class GameObjectPool : ObjectPooling<GameObject>
{
    [Required]
    GameObject prototype = null;
    public void SetPrototype(GameObject prototype)
    {
        this.prototype = prototype;
    }
    protected override void ActivateOjbect(GameObject target)
    {
        target.SetActive(true);
    }

    protected override GameObject CreateInstance()
    {
        return GameObject.Instantiate(this.prototype);
    }

    protected override void DeactivateObject(GameObject target)
    {
        target.SetActive(false);
    }
}
