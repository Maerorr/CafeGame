using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cup : HeldItem
{
    private List<Liquid> liquids = new List<Liquid>();
    public float MaxCapacity { get; private set; } = 1000f;

    private TextMeshPro text;
    
    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
        text = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        text.text = LiquidsToString();
    }

    public void AddLiquid(Liquid liquid, float amount)
    {
        if (GetTotalVolume() + amount > MaxCapacity)
        {
            //Handle overflow
            return;
        }
        
        if (HasLiquid(liquid.Name))
        {
            Liquid existingLiquid = liquids.Find(l => l.Name == liquid.Name);
            existingLiquid.Amount += amount;
            
        }
        else
        {
            // add liquid to the cup if it doesn't already exist
            liquid.Amount = amount;
            liquids.Add(liquid);
        }
    }

    // this returns true if the liquid was added, false if liquid doesn't exist
    public bool AddLiquid(string liquid_name, float amount)
    {
        if (GetTotalVolume() + amount > MaxCapacity)
        {
            //Handle overflow
            return true;
        }
        
        if (HasLiquid(liquid_name))
        {
            Liquid existingLiquid = liquids.Find(l => l.Name == liquid_name);
            existingLiquid.Amount += amount;
            return true;
        }

        return false;
    }

    public float GetTotalVolume()
    {
        float totalVolume = 0f;
        foreach (Liquid liquid in liquids)
        {
            totalVolume += liquid.Amount;
        }
        return totalVolume;
    }

    public bool HasLiquid(string liquidName)
    {
        return liquids.Exists(liquid => liquid.Name == liquidName);
    }

    public List<Liquid> GetLiquidsOfType(LiquidType type)
    {
        return liquids.FindAll(liquid => liquid.Type == type);
    }
    
    public float GetAmountOfLiquidByType(LiquidType type)
    {
        float totalVolume = 0f;
        var liquids = GetLiquidsOfType(type);
        foreach (Liquid liquid in liquids)
        {
            totalVolume += liquid.Amount;
        }
        return totalVolume;
    }

    public List<Liquid> GetLiquids()
    {
        return liquids;
    }

    public void EmptyCup()
    {
        liquids.Clear();
    }
    
    private string LiquidsToString()
    {
        string s = "";
        foreach (Liquid liquid in liquids)
        {
            s += liquid.Name + ", " + liquid.Amount + "ml\n";
        }
        return s;
    }
}
