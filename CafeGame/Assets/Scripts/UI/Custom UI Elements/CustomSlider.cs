using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSlider : MonoBehaviour
{
    [SerializeField]
    private GameObject slider_bar;

    [SerializeField] 
    private GameObject slider_background;
    
    private Transform slider_bar_transform;
    private SpriteRenderer slider_bar_sprite_renderer;
    private SpriteRenderer slider_background_sprite_renderer;
    
    private Transform slider_bar_default_transform;

    [SerializeField, Range(0.0f, 1.0f)]
    private float value = 0.0f;
    
    // Start is called before the first frame update
    void Awake()
    {
        slider_bar_transform = slider_bar.transform;
        slider_bar_default_transform = slider_bar_transform;
        slider_bar_sprite_renderer = slider_bar.GetComponentInChildren<SpriteRenderer>();
        slider_background_sprite_renderer = slider_background.GetComponentInChildren<SpriteRenderer>();
        if (slider_bar_sprite_renderer == null)
        {
            Debug.Log($"{name}: Slider SpriteRenderer is null!");
        }
        if (slider_background_sprite_renderer == null)
        {
            Debug.Log($"{name}: Slider Background SpriteRenderer is null!");
        }
    }
    
    public void SetSliderValue(float new_value)
    {
        value = Mathf.Clamp(new_value, 0.0f, 1.0f);
        UpdateSliderBar();
    }

    private void UpdateSliderBar()
    {
        slider_bar_transform.localScale = new Vector3(value, 1, 1);
    }
    
    public void SetSliderColor(Color color)
    {
        slider_bar_sprite_renderer.color = color;
    }
    
    public void ShowSlider()
    {
        slider_background_sprite_renderer.enabled = true;
        slider_bar_sprite_renderer.enabled = true;
    }
    
    public void HideSlider()
    {
        slider_background_sprite_renderer.enabled = false;
        slider_bar_sprite_renderer.enabled = false;
    }
    
}
