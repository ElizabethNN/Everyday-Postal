using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    void FixedUpdate()
    {
        var position = transform.position;
        var horizontal = position.x + Input.GetAxis("Horizontal") * speed;
        var vertical = position.z + Input.GetAxis("Vertical") * speed;
        transform.position = new Vector3(horizontal, 0, vertical);
    }
}
