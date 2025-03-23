using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField]
    float sensitivity = 500;

    [SerializeField]
    Transform body;

    float xRot;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        xRot -= Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;
        xRot = Mathf.Clamp(xRot, -90, 90);
        float yRot = Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;
        transform.localEulerAngles = new Vector3(xRot, 0, 0);
        body.Rotate(0, yRot, 0);
    }
}
