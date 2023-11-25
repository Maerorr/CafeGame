using ScriptableObjects;

// this will be expanded later to include more types of liquids
public enum LiquidType
{
    Coffee,
    Milk,
    Water
}

public class Liquid
{
    public string Name { get; set; }
    public LiquidType Type { get; set; }
    public float Amount { get; set; }
    public Liquid(string name, LiquidType type)
    {
        Name = name;
    }
}

public class Coffee : Liquid
{
    
    CoffeeData coffee_data;
    
    // this will later determine the 'quality' of the coffee made
    private float extraction;
    
    public Coffee(CoffeeData cd) : base("Coffee", LiquidType.Coffee)
    {
        base.Name = cd.ToString();
        coffee_data = cd;
    }
}

public class Milk : Liquid
{
    public Milk() : base("Milk", LiquidType.Milk)
    {
    }
}

public class Water : Liquid
{
    public Water() : base("Water", LiquidType.Water)
    {
    }
}