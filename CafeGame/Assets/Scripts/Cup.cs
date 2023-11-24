using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : HeldItem
{
    private List<Liquid> liquids = new List<Liquid>();
    public float MaxCapacity { get; private set; } = 1000f;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
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

    public List<Liquid> GetLiquids()
    {
        return liquids;
    }

    public void EmptyCup()
    {
        liquids.Clear();
    }
}
