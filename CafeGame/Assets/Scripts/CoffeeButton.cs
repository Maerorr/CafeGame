using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CoffeeButton : MonoBehaviour
{   
    SpriteRenderer spriteRenderer;
    Color pressedColor = new Color(0.1f, 0.9f, 0.1f, 1.0f);
    Color hoverColor = new Color(0.7f, 0.9f, 0.7f, 1.0f);
    Color defaultColor = Color.white;
    
    TestCoffeeMechanics testCoffeeMechanics;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        testCoffeeMechanics = GameObject.Find("CoffeeMachine").GetComponent<TestCoffeeMechanics>();
    }
    
    void OnMouseDown(){
        spriteRenderer.color = pressedColor;
        testCoffeeMechanics.StartPouring();
    }
    
    void OnMouseUp(){
        spriteRenderer.color = hoverColor;
        testCoffeeMechanics.StopPouring();
    }

    private void OnMouseEnter()
    {
        spriteRenderer.color = hoverColor;
    }
    
    private void OnMouseExit()
    {
        spriteRenderer.color = defaultColor;
    }
}
