using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class GroundCoffee : MonoBehaviour
{
    CoffeeData coffee_data;

    private float grind_size;
    private float weight;
    private bool is_used;
    
    void Start()
    {
        grind_size = 0;
        weight = 0;
        is_used = false;
    }
    
    void Update()
    {
        
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
