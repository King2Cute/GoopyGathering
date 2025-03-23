using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MuliplayerController : MonoBehaviour
{

    public static MuliplayerController instance;
    public List<Transform> PlayerTransforms = new();

    public Action<PickupItem.ItemType> InventoryUpdateEvent;
    public int SlimesEaten = 0;
    public int Keys = 0;
    public Action SlimeEaten;
    public Action KeyCollected;
    public Action OpenTavern;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Prevent duplicate instances
            return;
        }

        instance = this;
    }

   

    public Transform GetPlayerTransform(int playerNumber)
    {
        if(playerNumber >= PlayerTransforms.Count)
        {
            return null;
        }
        return PlayerTransforms[playerNumber];
    }

    public void QuestCompleted(PickupItem.ItemType itemType)
    {
        InventoryUpdateEvent?.Invoke(itemType);
    }


}
