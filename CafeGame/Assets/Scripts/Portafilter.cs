using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
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
    
    void Start()
    {
        base.Start();
        ground_coffee = null;
        text = GetComponentInChildren<TextMeshPro>();
    }
    
    void Update()
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
        ground_coffee = null;
        return true;
    }

    public bool GroundsUsed()
    {
        return ground_coffee.IsUsed();
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
}
