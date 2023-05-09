using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StickMovement : MonoBehaviour
{

    [SerializeField] 
    private GameObject trackObject;

    private Vector3 _delta;
    
    void Start()
    {
        _delta = transform.position - trackObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (trackObject.IsDestroyed())
            return;
        transform.position = trackObject.transform.position + _delta;
    }
}
