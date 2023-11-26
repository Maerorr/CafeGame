using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TamperLever : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] 
    private Transform lever;
    [SerializeField]
    private Transform lever_handle;
    
    [SerializeField]
    private Transform lever_pivot;
    
    [SerializeField]
    private float max_pull_speed = 0.1f;

    private float default_lever_y;
    private float default_handle_y;
    
    private Coroutine pull_coroutine;
    private Coroutine return_coroutine;

    private void Start()
    {
        default_lever_y = lever.position.y;
        default_handle_y = lever_handle.position.y;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pull();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopPull();
    }

    void Pull()
    {
        if (return_coroutine != null)
        {
            StopCoroutine(return_coroutine);
        }
        pull_coroutine = StartCoroutine(PullRoutine());
    }

    void StopPull()
    {
        StopCoroutine(pull_coroutine);
        return_coroutine = StartCoroutine(ReturnRoutine());
    }

    IEnumerator PullRoutine()
    {
        // check how far away mouse is from the handle only
        while (true)
        {
            var mouse_pos = Input.mousePosition;
            // convert mouse pos to world pos
            var mouse_world_pos = Camera.main.ScreenToWorldPoint(mouse_pos);
            var handle_pos = lever_handle.position;
            var y_diff = mouse_world_pos.y - handle_pos.y;
            
            Debug.Log($"{handle_pos.y}, {lever_pivot.position.y}");
            if (handle_pos.y >= lever_pivot.position.y)
            {
                // move the lever and handle down
                var move = new Vector3(0, Mathf.Clamp(y_diff, -max_pull_speed, 0.0f) * Time.deltaTime, 0);
                lever.position += move;
                lever_handle.position += move;
            }
        
            yield return null;
        }
    }
    
    IEnumerator ReturnRoutine()
    {
        while (true)
        {   
            // return to default position
            var move = new Vector3(0, max_pull_speed * Time.deltaTime, 0);
            lever.position += move;
            lever_handle.position += move;
            
            if (lever.position.y >= default_lever_y)
            {
                lever.position = new Vector3(lever.position.x, default_lever_y, lever.position.z);
                lever_handle.position = new Vector3(lever_handle.position.x, default_handle_y, lever_handle.position.z);
                break;
            }
        
            yield return null;
        }
    }
}
