using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    InventoryManeger InventoryManager;

    private void Start()
    {
        InventoryManager = GetComponentInChildren<InventoryManeger>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<PickupItem>(out var item))
        {
            InventoryManager.PickupItem(item);
        }
    }

}
