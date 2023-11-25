using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

enum HitType
{
    None,
    Interactive,
    Pickable,
    Placeable,
}

public class Hand : MonoBehaviour
{
    PlayerControls controls;

    private TextMeshPro text;

    private HitType hit = HitType.None;
    private RaycastHit2D raycast_hit;
    private InteractionInput hit_interaction_input = null;
    
    bool hand_empty = true;
    
    HeldItem held_item = null;

    private float start_z;
    
    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Enable();
        //controls.Player.LeftClick.performed += ctx => { Debug.Log(ctx); OnLeftClick(); };
        //controls.Player.RightClick.performed += ctx => { OnRightClick(); };
    }
    
    private void OnDisable()
    {
        controls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshPro>();
        text.text = "";
        start_z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // position = cursor position
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = start_z;
        transform.position = position;
        
        raycast_hit = Physics2D.Raycast(transform.position, Vector2.zero);
        if (raycast_hit.collider != null)
        {
            //Debug.Log(raycast_hit.transform.name);
            var tags = raycast_hit.transform.GetComponent<CustomTags>();
            if (tags is not null)
            {
                if (tags.Count() == 0)
                {
                    text.text = "";
                    hit = HitType.None;
                }
                if (tags.Count() == 1)
                {
                    switch (tags.Get(0))
                    {
                        case Tags.Interactive:
                            text.text = "interactive";
                            hit = HitType.Interactive;
                            break;
                        case Tags.Pickable:
                            text.text = "pickable";
                            hit = HitType.Pickable;
                            break;
                        case Tags.Placeable:
                            text.text = "placeable";
                            hit = HitType.Placeable;
                            break;
                    }
                }
                //Debug.Log($"{raycast_hit.transform.name} has tag {hit}");
                // in the future multiple tag checks can be possible

                // we checked the tags successfully, we can now return.
                // if something went wrong, we won't get there and we will get no hit
                return;
            }
        }
        
        text.text = "";
        hit = HitType.None;
    }

    void OnLeftClick()
    {
        switch (hit)
        {
            case HitType.Interactive:
                hit_interaction_input = raycast_hit.transform.GetComponentInParent<InteractionInput>();
                if (hand_empty)
                {
                    //Debug.Log("Interacting without item");
                    hit_interaction_input.Interact(null);
                }
                else
                {
                    //Debug.Log("Interacting with item");
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
            case HitType.Placeable:
                hit_interaction_input = raycast_hit.transform.GetComponentInParent<InteractionInput>();
                if (!hand_empty)
                {
                    hit_interaction_input.Interact(held_item);
                    // this being false means that the item cannot be snapped for a reason.
                    if (held_item.IsSnapped())
                    {
                        DropHeldItem();
                    }
                }
                else
                {
                    // can pick up the item
                    hit_interaction_input.Interact(held_item);
                }
                break;
        }
    }

    void OnRightClick()
    {
        if (!hand_empty)
        {
            DropHeldItem();
        }
    }

    void DropHeldItem()
    {
        held_item.Drop();
        hand_empty = true;
        held_item = null;
    }
    
    public void AssignHeldItem(HeldItem item)
    {
        held_item = item;
        held_item.PickUp();
        hand_empty = false;
    }
}
