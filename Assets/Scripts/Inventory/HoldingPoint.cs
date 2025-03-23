using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoldingPoint : MonoBehaviour
{
    [SerializeField]
    public PickupItem.ItemType itemType;

    [SerializeField]
    GameObject model;

    TextMeshProUGUI counterText;

    int numOfItems = 0;
    private void Start()
    {
        counterText = GetComponentInChildren<TextMeshProUGUI>();
        CheckItemCount();
    }
    public void AddItem()
    {
        numOfItems++;
        CheckItemCount();
    }
    
    public void ClearItems()
    {
        numOfItems = 0;
        CheckItemCount();
    }

    void CheckItemCount()
    {
        
        model.SetActive(numOfItems > 0);
        if (numOfItems <= 0)
        {
            counterText.text = "";
        }
        else
        {
            counterText.text = numOfItems.ToString();
        }
    }
}
