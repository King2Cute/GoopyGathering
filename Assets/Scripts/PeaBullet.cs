using System;
using UnityEngine;

public class PeaBullet : MonoBehaviour
{
    //private float maxAliveTime = 10f;
    private float speed = 2;

    private void Awake()
    {
        // Instantly rotate because pea pod is sideways
        transform.Rotate(0, -90, 0);
    }

    void Start()
    {
        Destroy(gameObject, 10f);
    }
    
    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * (speed * Time.deltaTime));
    }

    // destroy on collision of certain objs excluding player?
    private void OnCollisionEnter(Collision other)
    {
        
    }
}
