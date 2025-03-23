using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public ItemType type;


    public enum ItemType
    {
        KEY,
        CARROT,
        YOGURT,
        BANANA,
        DEFAULT,
    }
}
