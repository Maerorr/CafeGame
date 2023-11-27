using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldItem : MonoBehaviour
{
    bool picked_up = false;
    private bool snapped = false;
    
    Collider2D collider;
    
    private Rigidbody2D rb = null;
    
    private bool uses_physics;

    protected void Awake()
    {
        collider = GetComponentInChildren<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        uses_physics = false;
        if (rb != null)
        {
            uses_physics = true;
        }
    }
    
    protected void Update()
    {
        if (picked_up)
        {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = transform.position.z;
            transform.position = position;
            if (uses_physics)
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    public void PickUp()
    {
        picked_up = true;
        // disable collider
        collider.enabled = false;
        if (uses_physics)
        {
            transform.rotation = Quaternion.identity;
            rb.angularVelocity = 0;
        }
        // todo: turn possible physics off
    }

    public void Drop()
    {
        picked_up = false;
        if (snapped)
        {
            // if it was previously snapped, don't enable collider
            collider.enabled = false;
        }
        else
        {
            collider.enabled = true; 
        }
        
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
        transform.rotation = t.rotation;
        
        if (uses_physics)
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
    
    public void Unsnap()
    {
        snapped = false;
        collider.enabled = true;
        
        if (uses_physics)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
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
