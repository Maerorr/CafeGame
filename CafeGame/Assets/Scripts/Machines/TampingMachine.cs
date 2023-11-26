using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TampingMachine : MonoBehaviour
{
    [SerializeField]
    private Transform filter_snap_point;
    
    private Portafilter filter;
    
    [SerializeField] 
    private TextMeshPro error_text;
    Coroutine error_text_coroutine;

    public void SnapOrUnsnapFilter(HeldItem item)
    {
        //DisplayErrorText("Pressed SnapOrUnsnapFilter");
        
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
    }
    
    public void DisplayErrorText(string text)
    {
        if (error_text_coroutine != null)
            StopCoroutine(error_text_coroutine);
        error_text_coroutine = StartCoroutine(ShowTextForSeconds(text));
    }
    
    IEnumerator ShowTextForSeconds(string text)
    {
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
}
