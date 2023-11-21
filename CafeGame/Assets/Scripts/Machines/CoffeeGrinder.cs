using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;

public class CoffeeGrinder : MonoBehaviour
{
    [SerializeField]
    CoffeeData coffee_data;

    private float grind_size = 15.0f;
    private float dose_weight = 18.0f;

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
}
