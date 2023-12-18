using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CoffeeGrinder : MonoBehaviour
{
    [SerializeField]
    CoffeeData coffee_data;

    private float grind_size = 15.0f;
    private float dose_weight = 18.0f;
    
    [SerializeField]
    private TextMeshPro grind_size_text;
    
    [SerializeField]
    private TextMeshPro dose_weight_text;

    private void Start()
    {
        UpdateText();
    }

    public void GrindCoffee(HeldItem item)
    {
        if (item is null) return;
        // try to cast item to Portafiler class
        Portafilter portafilter = item as Portafilter;
        if (portafilter is null) return;
        // check if portafilter is empty
        if (!portafilter.IsEmpty()) return;
        GroundCoffee ground_coffee = new GroundCoffee(coffee_data, grind_size, dose_weight);
        portafilter.FillWithGrounds(ground_coffee);
    }
    
    public void IncreaseGrindSize()
    {
        grind_size += 0.5f;
        UpdateText();
    }
    
    public void DecreaseGrindSize()
    {
        grind_size -= 0.5f;
        UpdateText();
    }
    
    public void IncreaseDoseWeight()
    {
        dose_weight += 0.2f;
        UpdateText();
    }
    
    public void DecreaseDoseWeight()
    {
        dose_weight -= 0.2f;
        UpdateText();
    }
    
    private void UpdateText()
    {
        grind_size_text.text = grind_size.ToString("F1");
        dose_weight_text.text = dose_weight.ToString("F1") + " g";
    }
}
