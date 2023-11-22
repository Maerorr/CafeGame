using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspressoMachine : MonoBehaviour
{
    [SerializeField]
    private Transform filter_snap_point;
    [SerializeField]
    private Transform cup_snap_point;

    void SnapFilter(HeldItem item)
    {
        if (item is null) return;
        // try to cast item to Portafiler class
        Portafilter portafilter = item as Portafilter;
        if (portafilter is null) return;
        // check if portafilter is empty
        if (portafilter.IsEmpty()) return;
        if (portafilter.GroundsUsed()) return;
        
        portafilter.transform.position = filter_snap_point.position;
        
    }
}
