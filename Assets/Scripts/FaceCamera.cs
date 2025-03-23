using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{

    GameObject mainCam;
    private void Start()
    {
        mainCam = Camera.main.gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if (mainCam != null)
        {
            transform.LookAt(mainCam.transform.position);
        }
    }
}
