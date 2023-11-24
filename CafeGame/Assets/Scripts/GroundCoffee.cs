using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class GroundCoffee
{
    CoffeeData coffee_data;

    private float grind_size;
    private float weight;
    private bool is_used;
    
    public GroundCoffee(CoffeeData coffee_data, float grind_size, float weight)
    {
        this.coffee_data = coffee_data;
        this.grind_size = grind_size;
        this.weight = weight;
        is_used = false;
    }
    
    public float GetGrindSize()
    {
        return grind_size;
    }
    
    public float GetWeight()
    {
        return weight;
    }
    
    public bool IsUsed()
    {
        return is_used;
    }
    
    public CoffeeData GetCoffeeData()
    {
        return coffee_data;
    }
}
