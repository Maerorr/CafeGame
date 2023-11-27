using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TampingMachine : MonoBehaviour
{
    [SerializeField]
    private Transform filter_snap_point;

    [SerializeField] 
    private Transform tamper;
    private float default_tamper_y;
    
    private Portafilter filter;
    
    [SerializeField] 
    private TextMeshPro error_text;
    Coroutine error_text_coroutine;
    
    [SerializeField]
    float time_to_tamp = 2.0f;
    float tamping_time = 0;
    
    float tamping_status = 0;

    private void Awake()
    {
        default_tamper_y = tamper.transform.position.y;
    }

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

    public void SetTampingStatus((float, bool) values)
    {
        tamping_status = Mathf.Clamp(values.Item1, 0, 1);

        // move tamper between default position and filter snap point
        var tamper_position = tamper.position;
        var filter_snap_point_position = filter_snap_point.position;
        var new_tamp_y = Mathf.Lerp(default_tamper_y, filter_snap_point_position.y, tamping_status);
        tamper.position = new Vector3(tamper_position.x, new_tamp_y, tamper_position.z);

        if (values.Item2 == false)
        {
            tamping_time = 0;
        }
        else
        {
            if (tamping_status > 0.95f)
            {
                tamping_time += Time.deltaTime;
                if (tamping_time >= time_to_tamp)
                {
                    tamping_time = time_to_tamp;
                }
                var tamping_progress = tamping_time / time_to_tamp;
                filter.SetTampStrength(tamping_progress);
            }
        }
    }

}
