using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EspressoMachine : MonoBehaviour
{
    [SerializeField]
    private Transform filter_snap_point;
    [SerializeField]
    private Transform cup_snap_point;

    public void SnapFilter(HeldItem item)
    {
        if (item is null) return;
        // try to cast item to Portafiler class
        Portafilter portafilter = item as Portafilter;
        if (portafilter is null) return;
        // check if portafilter is empty
        if (portafilter.IsEmpty()) return;
        if (portafilter.GroundsUsed()) return;
        
        portafilter.SnapTo(filter_snap_point);
    }
}
