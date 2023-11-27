using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EspressoMachine : MonoBehaviour
{
    [SerializeField]
    private Transform filter_snap_point;
    [SerializeField]
    private Transform cup_snap_point;
    [SerializeField]
    private Transform water_snap_point;
    [SerializeField]
    private Transform milk_snap_point;

    [SerializeField] 
    private TextMeshPro error_text;
    Coroutine error_text_coroutine;
    
    private Portafilter filter = null;
    private Cup cup = null;

    static int debug_counter = 0;

    private bool is_brewing = false;
    private Coroutine brewing_coroutine;

    private bool is_pouring_water = false;
    private Coroutine pouring_water_coroutine;

    private bool is_pouring_milk = false;
    private Coroutine pouring_milk_coroutine;
    
    public void SnapOrUnsnapFilter(HeldItem item)
    {
        //DisplayErrorText("Pressed SnapOrUnsnapFilter");
        if (is_brewing)
        {
            return;
        }
        
        switch (filter)
        {
            case null:
                if (item is null)
                {
                    DisplayErrorText("Item Null!");
                    return;
                }
                Portafilter portafilter = item as Portafilter;
        
                if (portafilter is null)
                {
                    DisplayErrorText("Portafilter Null!");
                    return;
                }
                // check if portafilter is empty
                if (portafilter.IsEmpty())
                {
                    DisplayErrorText("Portafilter is empty!");
                    return;
                }
                if (portafilter.GroundsUsed())
                {
                    DisplayErrorText("Grounds have been already used!");
                    return;
                }
                portafilter.SnapTo(filter_snap_point);
                filter = portafilter;
                break;
            default:
                filter.Unsnap();
                PlayerManager.Instance.GetPlayerHand().AssignHeldItem(filter);
                filter = null;
                break;
        }
        debug_counter++;
    }

    public void SnapOrUnsnapCup(HeldItem item)
    {
        switch (cup) {
            case null:
                if (item is null) return;
                Cup cast_cup = item as Cup;
                if (cast_cup is null) return;
                if (cup != null)
                {
                    DisplayErrorText("Cup is already in place!");
                    return;
                }
                cast_cup.SnapTo(cup_snap_point);
                cup = cast_cup;
                break;
            default:
                cup.Unsnap();
                PlayerManager.Instance.GetPlayerHand().AssignHeldItem(cup);
                cup = null;
                break;
        }
        
    }

    public void SnapOrUnsnapCupWater(HeldItem item)
    {
        switch (cup) {
            case null:
                if (item is null) return;
                Cup cast_cup = item as Cup;
                if (cast_cup is null) return;
                if (cup != null)
                {
                    DisplayErrorText("Cup is already in place!");
                    return;
                }
                cast_cup.SnapTo(water_snap_point);
                cup = cast_cup;
                break;
            default:
                cup.Unsnap();
                PlayerManager.Instance.GetPlayerHand().AssignHeldItem(cup);
                cup = null;
                break;
        }
        
    }

    public void SnapOrUnsnapCupMilk(HeldItem item)
    {
        switch (cup) {
            case null:
                if (item is null) return;
                Cup cast_cup = item as Cup;
                if (cast_cup is null) return;
                if (cup != null)
                {
                    DisplayErrorText("Cup is already in place!");
                    return;
                }
                cast_cup.SnapTo(milk_snap_point);
                cup = cast_cup;
                break;
            default:
                cup.Unsnap();
                PlayerManager.Instance.GetPlayerHand().AssignHeldItem(cup);
                cup = null;
                break;
        }
        
    }
    
    public void DisplayErrorText(string text)
    {
        if (error_text_coroutine != null)
            StopCoroutine(error_text_coroutine);
        error_text_coroutine = StartCoroutine(ShowTextForSeconds(text));
    }
    
    IEnumerator ShowTextForSeconds(string text)
    {
        error_text.color = new Color(error_text.color.r, error_text.color.g, error_text.color.b, 1);
        error_text.text = text;
        yield return new WaitForSeconds(1);
        var i = 0;
        while (error_text.color.a >= 0)
        {
            // fade alpha to 0
            error_text.color = new Color(error_text.color.r, error_text.color.g, error_text.color.b, error_text.color.a - 0.01f);
            if (error_text.color.a <= 0)
            {
                error_text.text = "";
                error_text.color = new Color(error_text.color.r, error_text.color.g, error_text.color.b, 1);
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
        error_text.text = "";
        error_text.color = new Color(error_text.color.r, error_text.color.g, error_text.color.b, 1);
    }

    IEnumerator PourCoffee()
    {
        while (true)
        {
            var grounds = filter.GetGroundsData();
            var name = grounds.coffee_data.ToString();

            if (!cup.AddLiquid(name, 0.5f))
            {
                Liquid coffee = new Coffee(grounds.coffee_data);
                cup.AddLiquid(coffee, 0.5f);
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator PourWaterCoroutine()
    {
        while (true)
        {
            if (!cup.AddLiquid("Water", 0.5f))
            {
                Liquid water = new Water();
                cup.AddLiquid(water, 0.5f);
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator PourMilkCoroutine()
    {
        while (true)
        {
            if (!cup.AddLiquid("Milk", 0.5f))
            {
                Liquid milk = new Milk();
                cup.AddLiquid(milk, 0.5f);
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
    
    public void Brew(HeldItem i)
    {
        if (cup is null)
        {
            DisplayErrorText("No cup in place!");
            return;
        }
        if (filter is null)
        {
            DisplayErrorText("No filter in place!");
            return;
        }
        if (is_brewing)
        {
            StopCoroutine(brewing_coroutine);
            is_brewing = false;
        }
        else
        {
            brewing_coroutine = StartCoroutine(PourCoffee());
            is_brewing = true;
        }
    }

    public void PourWater(HeldItem i)
    {
        if (is_pouring_water)
        {
            StopCoroutine(pouring_water_coroutine);
            is_pouring_water = false;
        }
        else
        {
            pouring_water_coroutine = StartCoroutine(PourWaterCoroutine());
            is_pouring_water = true;
        }
    }

    public void PourMilk(HeldItem i)
    {
        if (is_pouring_milk)
        {
            StopCoroutine(pouring_milk_coroutine);
            is_pouring_milk = false;
        }
        else
        {
            pouring_milk_coroutine = StartCoroutine(PourMilkCoroutine());
            is_pouring_milk = true;
        }
    }

}
