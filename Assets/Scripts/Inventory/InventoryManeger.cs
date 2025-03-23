using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManeger : MonoBehaviour
{
    List<GameObject> ItemTypes;

    List<HoldingPoint> HoldingPoints = new();
    [SerializeField]
    float RotationSpeed = 10;

    private void Start()
    {
        HoldingPoints = new List<HoldingPoint>(GetComponentsInChildren<HoldingPoint>());
    }

    private void OnEnable()
    {
    }
    private void OnDisable()
    {
        //MuliplayerController.instance.InventoryUpdateEvent -= QuestCompleted;
    }

    bool subscribed = false;
    private void Update()
    {
        if (!subscribed)
        {
            MuliplayerController.instance.InventoryUpdateEvent += QuestCompleted;
            subscribed = true;
        }
        Quaternion target = Quaternion.Euler(0, transform.rotation.eulerAngles.y + RotationSpeed, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * RotationSpeed);
    }


    public void PickupItem(PickupItem item)
    {
        foreach(var holdingPoint in HoldingPoints)
        {
            if(holdingPoint.itemType == item.type)
            {
                holdingPoint.AddItem();
            }
        }
    }

    void QuestCompleted(PickupItem.ItemType itemType)
    {
        foreach (var holdingPoint in HoldingPoints)
        {
            if (holdingPoint.itemType == itemType)
            {
                holdingPoint.ClearItems();
            }
        }
    }
}
