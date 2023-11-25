using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CameraCollider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    UnityEvent hover_event;
    
    [SerializeField]
    UnityEvent exit_event;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        hover_event.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
        exit_event.Invoke();
    }
}
