using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBox : MonoBehaviour
{
    public void KnockCoffee(HeldItem item)
    {
        if (item is null) return;
        // try to cast item to Portafiler class
        Portafilter portafilter = item as Portafilter;
        if (portafilter is null) return;
        // check if portafilter is empty
        if (portafilter.IsEmpty()) return;
        portafilter.EmptyIntoTrash();
    }
}
