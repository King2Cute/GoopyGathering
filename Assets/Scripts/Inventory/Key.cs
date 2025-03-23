using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : PickupItem
{
    [SerializeField]
    Collider Collider;

    private void Start()
    {
        if(Collider == null)
        {
            Collider = GetComponent<Collider>();
        }
    }

    public void Pickup(Transform parent, Vector3 positionOffset)
    {
        transform.parent = parent;
        transform.position = parent.position + positionOffset;
        Collider.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<LockedCamPlayerMovement>(out var script))
        {
            gameObject.SetActive(false);
            MuliplayerController.instance.Keys++;
            MuliplayerController.instance.KeyCollected?.Invoke();
            
        }
    }
}
