using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/**
 * 
 */
public class OnCreateTrigger : MonoBehaviour
{
    [SerializeField]
    UnityEvent triggerEvent = null;
    // Start is called before the first frame update
    void Start()
    {
        if (triggerEvent != null) {
            triggerEvent.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
