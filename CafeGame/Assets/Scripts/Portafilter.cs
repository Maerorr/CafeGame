using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GroundsData
{
    public float grind_size;
    public float weight;
    
    public float best_grind_size;
    public float best_dose_weight;
    public float best_temperature;
}

public class Portafilter : MonoBehaviour
{
    GroundCoffee ground_coffee;
    
    void Start()
    {
        ground_coffee = null;
    }
    
    void Update()
    {
        // nothing
    }

    bool IsEmpty()
    {
        if (ground_coffee is null) return true;
        return false;
    }

    bool FillWithGrounds(GroundCoffee coffee)
    {
        if (!IsEmpty()) return false;
        ground_coffee = coffee;
        return true;
    }
    
    bool EmptyIntoTrash()
    {
        if (IsEmpty()) return false;
        ground_coffee = null;
        return true;
    }

    bool GroundsUsed()
    {
        return ground_coffee.IsUsed();
    }
    
    GroundsData GetGroundsData()
    {
        var grounds_best_settings = ground_coffee.GetCoffeeData();
        GroundsData grounds_data = new GroundsData();
        grounds_data.grind_size = ground_coffee.GetGrindSize();
        grounds_data.weight = ground_coffee.GetWeight();
        grounds_data.best_grind_size = grounds_best_settings.best_grind_size;
        grounds_data.best_dose_weight = grounds_best_settings.best_dose_weight;
        grounds_data.best_temperature = grounds_best_settings.best_temperature;
        return grounds_data;
    }
}
