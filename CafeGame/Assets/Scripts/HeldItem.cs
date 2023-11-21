using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldItem : MonoBehaviour
{
    bool picked_up = false;
    
    Collider2D collider;

    public void Start()
    {
        collider = GetComponentInChildren<Collider2D>();
    }
    
    public void Update()
    {
        if (picked_up)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            transform.position = position;
        }
    }

    public void PickUp()
    {
        picked_up = true;
        // disable collider
        collider.enabled = false;
        // todo: turn possible physics off
    }

    public void Drop()
    {
        picked_up = false;
        collider.enabled = true;
        // todo: turn possible physics on
    }
}
