using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.InputSystem;

enum HitType
{
    None,
    Interactive,
    Pickable
}

public class Hand : MonoBehaviour
{
    PlayerControls controls;
    
    private SpriteRenderer sr = null;

    private HitType hit = HitType.None;
    private RaycastHit2D raycast_hit;
    private InteractionInput hit_interaction_input = null;
    
    bool hand_empty = true;
    
    HeldItem held_item = null;

    
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    
    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.LeftClick.performed += ctx => OnLeftClick();
        controls.Player.RightClick.performed += ctx => OnRightClick();
    }
    
    private void OnDisable()
    {
        controls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        sr.color = new Color(96, 141, 192);
    }

    // Update is called once per frame
    void Update()
    {
        // position = cursor position
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        transform.position = position;
        
        raycast_hit = Physics2D.Raycast(transform.position, Vector2.zero);
        if (raycast_hit.collider != null)
        {
            if (raycast_hit.transform.CompareTag("Interactive"))
            {
                sr.color = Color.green;
                hit = HitType.Interactive;
            } else if (raycast_hit.transform.CompareTag("Pickable"))
            {
                sr.color = Color.yellow;
                hit = HitType.Pickable;
            }
            else
            {
                hit = HitType.None;
            }
        }
        else
        {
            sr.color = new Color(96, 141, 192);
            hit = HitType.None;
        }
    }

    void OnLeftClick()
    {
        switch (hit)
        {
            case HitType.Interactive:
                hit_interaction_input = raycast_hit.transform.GetComponentInParent<InteractionInput>();
                if (hand_empty)
                {
                    hit_interaction_input.Interact(null);
                }
                else
                {
                    hit_interaction_input.Interact(held_item);
                }
                break;
            case HitType.Pickable:
                
                if (hand_empty)
                {
                    held_item = raycast_hit.transform.GetComponentInParent<HeldItem>();
                    
                    held_item.PickUp();
                    hand_empty = false;
                }
                break;
        }
    }

    void OnRightClick()
    {
        if (!hand_empty)
        {
            held_item.Drop();
            hand_empty = true;
            held_item = null;
        }
    }
}
