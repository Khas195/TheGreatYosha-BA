using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    [Required]
    GameObject behaviorsNode = null;
    [SerializeField]
    [Required]
    GameObject modelNode = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
