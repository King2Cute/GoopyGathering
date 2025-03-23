using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernOpen : MonoBehaviour
{
    private void Awake()
    {
        MuliplayerController.instance?.OpenTavern?.Invoke();
    }
}
