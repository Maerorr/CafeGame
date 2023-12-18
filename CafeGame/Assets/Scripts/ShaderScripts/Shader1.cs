using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shader1 : MonoBehaviour
{
    Material material;
    public Texture2D tex;

    [SerializeField] public float coffee_percent = 1f;
    [SerializeField] public float milk_percent = 0f;
    [SerializeField] public float water_percent = 0f;
    
    public float fill_percent = 0f;
    
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        
        material.SetFloat("_coffee_percentage", coffee_percent);
        material.SetFloat("_milk_percentage", milk_percent);
        material.SetFloat("_water_percentage", water_percent);
    }
    
    void Update()
    {
        material.SetFloat("_coffee_percentage", coffee_percent);
        material.SetFloat("_milk_percentage", milk_percent);
        material.SetFloat("_water_percentage", water_percent);
        material.SetFloat("_fill_amount", fill_percent);
    }
    
    public void SetCoffeePercent(float percent)
    {
        coffee_percent = percent;
    }
    
    public void SetMilkPercent(float percent)
    {
        milk_percent = percent;
    }
    
    public void SetWaterPercent(float percent)
    {
        water_percent = percent;
    }
    
    public void SetFillPercent(float percent)
    {
        fill_percent = percent;
    }
}
