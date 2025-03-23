using System.Collections.Generic;
using UnityEngine;

public class TheBigSuck : MonoBehaviour
{
    public float suckRange = 10f;
    public float suckForce = 5f;
    public float sizeIncrease = 0.2f;
    public LayerMask suckableLayer;

    [SerializeField]
    KeyCode Suck = KeyCode.Q;

    void Update()
    {
        if (Input.GetKey(Suck))
        {
            SuckObjects();
        }
    }

    void SuckObjects()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, suckRange, suckableLayer);

        foreach (Collider obj in objects)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (transform.position - obj.transform.position).normalized;
                rb.AddForce(direction * suckForce, ForceMode.Acceleration);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<SuckableNPC>(out var suckableNPC))
        {
            suckableNPC.IsSucked = true;
            other.gameObject.SetActive(false);
            transform.localScale += new Vector3(sizeIncrease, sizeIncrease, sizeIncrease);
            MuliplayerController.instance.SlimesEaten++;
            MuliplayerController.instance.SlimeEaten?.Invoke();
        }
    }
}
