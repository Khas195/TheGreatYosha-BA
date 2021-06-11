using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class SpawnEffectRandomly : MonoBehaviour
{
    [SerializeField]
    [Required]
    GameObject centerOfHost = null;
    [SerializeField]
    float rangeMin = 0;
    [SerializeField]
    float rangeMax = 0;
    [SerializeField]
    float randomIntervalMin = 1f;
    [SerializeField]
    float randomInvtervalMax = 2f;
    [SerializeField]
    VFXResources.VFXList effectTag = VFXResources.VFXList.ChickenDeath;

    float curTime = 0;
    float curInterval = 1.5f;


    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(centerOfHost.transform.position, rangeMin);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(centerOfHost.transform.position, rangeMax);
    }
    // Update is called once per frame
    void Update()
    {
        if (curTime >= curInterval)
        {
            var vfx = VFXSystem.GetInstance();
            if (vfx)
            {
                var randomRange = Random.Range(rangeMin, rangeMax);
                var randomPos = centerOfHost.transform.position + Random.insideUnitSphere * randomRange;
                vfx.PlayEffect(effectTag, randomPos, Quaternion.identity);
            }
            curInterval = Random.Range(randomIntervalMin, randomInvtervalMax);
            curTime = 0;
        }
        curTime += Time.deltaTime;
    }
}
