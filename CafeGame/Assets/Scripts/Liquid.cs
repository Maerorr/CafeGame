using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Liquid
{
    public string Name { get; set; }
    public float Amount { get; set; }
    public Liquid(string name)
    {
        Name = name;
    }
}

public class Coffee : Liquid
{
    public Coffee(string name) : base("Coffee")
    {
        
    }
}

public class Milk : Liquid
{
    public Milk(string name) : base("Milk")
    {
    }
}