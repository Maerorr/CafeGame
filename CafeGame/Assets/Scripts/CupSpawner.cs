using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupSpawner : MonoBehaviour
{
    [SerializeField]
    Cup cup_object;

    public void SpawnCup(HeldItem item)
    {
        // if hand is NOT empty, dont spawn a cup to hand
        if (item is not null) return;
        // spawn a cup to hand
        // create a cup object
        Cup cup = Instantiate(cup_object, transform.position, transform.rotation);
        // set cup's parent to hand
        PlayerManager.Instance.GetPlayerHand().AssignHeldItem(cup);
    }
}
