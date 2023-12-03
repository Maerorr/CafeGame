using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;

public class CoffeeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    Color pressedColor = new Color(0.1f, 0.9f, 0.1f, 1.0f);
    Color hoverColor = new Color(0.7f, 0.9f, 0.7f, 1.0f);
    Color defaultColor = Color.white;

    private Light2D light;
    
    [SerializeField]
    UnityEvent OnClick;
    
    void Start()
    {
        light = GetComponentInChildren<Light2D>();
        if (light != null)
        {
            light.intensity = 0.0f;
            light.color = defaultColor;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (light != null)
        {
            light.intensity = 1.0f;
            light.color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (light != null)
        {
            light.color = defaultColor;
            light.intensity = 0.0f;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (light != null)
        {   
            light.color = pressedColor;
        }
        OnClick.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (light != null)
        {   
            light.color = hoverColor;
        }
    }
}
