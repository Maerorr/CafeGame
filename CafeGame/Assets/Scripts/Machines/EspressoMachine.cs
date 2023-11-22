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
    private TextMeshPro error_text;
    
    Portafilter filter = null;

    public void SnapFilter(HeldItem item)
    {
        if (item is null) return;
        // try to cast item to Portafiler class
        Portafilter portafilter = item as Portafilter;
        if (portafilter is null) return;
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
    }
    
    public void DisplayErrorText(string text)
    {
        StartCoroutine(ShowTextForSeconds(text));
    }
    
    IEnumerator ShowTextForSeconds(string text)
    {
        error_text.text = text;
        yield return new WaitForSeconds(1);
        while (true)
        {
            // fade alpha to 0
            error_text.color = new Color(error_text.color.r, error_text.color.g, error_text.color.b, error_text.color.a - 0.1f);
            if (error_text.color.a <= 0)
            {
                error_text.text = "";
                error_text.color = new Color(error_text.color.r, error_text.color.g, error_text.color.b, 1);
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        
    }
}
