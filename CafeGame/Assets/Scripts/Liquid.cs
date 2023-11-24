using ScriptableObjects;

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
    
    CoffeeData coffee_data;
    
    // this will later determine the 'quality' of the coffee made
    private float extraction;
    
    public Coffee(CoffeeData cd) : base("Coffee")
    {
        base.Name = cd.ToString();
        coffee_data = cd;
    }
}

public class Milk : Liquid
{
    public Milk() : base("Milk")
    {
    }
}

public class Water : Liquid
{
    public Water() : base("Water")
    {
    }
}