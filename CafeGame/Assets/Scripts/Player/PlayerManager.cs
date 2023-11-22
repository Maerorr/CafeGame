using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    
    public static PlayerManager Instance
    {
        get
        {
            // If the instance doesn't exist, create it
            if (instance == null)
            {
                // Search for an existing instance in the scene
                instance = FindObjectOfType<PlayerManager>();

                // If no instance was found, create a new GameObject and attach the class to it
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("Player Manager");
                    instance = singletonObject.AddComponent<PlayerManager>();
                }
            }

            return instance;
        }
    }

    private Hand player;

    private void Awake()
    {
        // If the instance already exists and it's not this one, destroy this one
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Hand>();
    }

    public Hand GetPlayerHand()
    {
        return player;
    }
}

