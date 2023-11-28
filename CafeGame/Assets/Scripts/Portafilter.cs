using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public struct GroundsData
{
    public CoffeeData coffee_data;
        
    public float grind_size;
    public float weight;
}

public class Portafilter : HeldItem
{
    GroundCoffee ground_coffee;

    private TextMeshPro text;
    
    private float tamp_strength = 0.0f;
    
    [SerializeField]
    CustomSlider tamp_slider;
    
    private new void Awake()
    {
        base.Awake();
        ground_coffee = null;
        text = GetComponentInChildren<TextMeshPro>();
    }

    private void Start()
    {
        tamp_slider.HideSlider();
    }

    new void Update()
    {
        base.Update();
        if (ground_coffee is null)
        {
            text.text = "";
        }
        else
        {
            text.text = String.Format("{0}, {1}g",ground_coffee.GetCoffeeData().name, ground_coffee.GetWeight());
        }
    }

    public bool IsEmpty()
    {
        if (ground_coffee is null) return true;
        return false;
    }

    public bool FillWithGrounds(GroundCoffee coffee)
    {
        if (!IsEmpty()) return false;
        ground_coffee = coffee;
        return true;
    }
    
    public bool EmptyIntoTrash()
    {
        if (IsEmpty()) return false;
        tamp_strength = 0.0f;
        ground_coffee = null;
        tamp_slider.HideSlider();
        return true;
    }

    public bool GroundsUsed()
    {
        return ground_coffee.IsUsed();
    }
    
    public bool IsTamped()
    {
        if (tamp_strength < 0.999f) return false;
        return true;
    }
    
    public GroundsData GetGroundsData()
    {
        var grounds_best_settings = ground_coffee.GetCoffeeData();
        GroundsData grounds_data = new GroundsData();
        grounds_data.grind_size = ground_coffee.GetGrindSize();
        grounds_data.weight = ground_coffee.GetWeight();
        grounds_data.coffee_data = ground_coffee.GetCoffeeData();
        return grounds_data;
    }
    
    public void SetTampStrength(float strength)
    {
        if (strength < tamp_strength) return;
        tamp_strength = strength;
        tamp_slider.SetSliderValue(tamp_strength);
        if (tamp_strength < 0.001f)
        {
            tamp_slider.HideSlider();
            return;
        }

        tamp_slider.ShowSlider();
        
        if (tamp_strength < 1.0f)
        {
            tamp_slider.SetSliderColor(Color.red);
        }
        else
        {
            tamp_slider.SetSliderColor(Color.green);
        }
    }

    public void AddTampStrength(float strength)
    {
        
        tamp_strength += strength;
        tamp_strength = Mathf.Clamp(tamp_strength, 0, 1);
        tamp_slider.SetSliderValue(tamp_strength);
        if (tamp_strength < 0.001f)
        {
            tamp_slider.HideSlider();
            return;
        }

        tamp_slider.ShowSlider();
        
        if (tamp_strength < 1.0f)
        {
            tamp_slider.SetSliderColor(Color.red);
        }
        else
        {
            tamp_slider.SetSliderColor(Color.green);
        }
    }
}
