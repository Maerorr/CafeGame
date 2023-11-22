using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldItem : MonoBehaviour
{
    bool picked_up = false;
    private bool snapped = false;
    
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
            position.z = transform.position.z;
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

    public void SnapTo(Transform t)
    {
        picked_up = false;
        collider.enabled = false;
        snapped = true;
        
        // snap positions but ignore z
        Vector3 position = t.position;
        position.z = transform.position.z;
        transform.position = position;
    }
    
    public bool IsPickedUp()
    {
        return picked_up;
    }
    
    public bool IsSnapped()
    {
        return snapped;
    }
}
