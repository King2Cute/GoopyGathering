using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCamController : MonoBehaviour
{

    [SerializeField]
    List<Transform> PlayerTransforms => MuliplayerController.instance.PlayerTransforms;

    public float maxDistanceX = 5f;
    public float maxDistanceZ = 5f;

    // Update is called once per frame
    void Update()
    {
        transform.position = FindPlayerAverage();
        ConstrainPlayerDistance();
    }

    Vector3 FindPlayerAverage()
    {
        Vector3 pos = Vector3.zero;
        if(PlayerTransforms.Count == 0)
        { 
            return pos;
        }
        foreach(var playerTransform in PlayerTransforms)
        {
            pos += playerTransform.position;
        }
        return pos / PlayerTransforms.Count;
    }

    void ConstrainPlayerDistance()
    {
        if (PlayerTransforms.Count < 2) return;

        Transform playerA = PlayerTransforms[0];
        Transform playerB = PlayerTransforms[1];

        Vector3 difference = playerB.position - playerA.position;

        if (Mathf.Abs(difference.x) > maxDistanceX)
        {
            float excessX = Mathf.Abs(difference.x) - maxDistanceX;
            float directionX = Mathf.Sign(difference.x);

            playerA.position += new Vector3(directionX * excessX * 0.5f, 0, 0);
            playerB.position -= new Vector3(directionX * excessX * 0.5f, 0, 0);
        }

        if (Mathf.Abs(difference.z) > maxDistanceZ)
        {
            float excessZ = Mathf.Abs(difference.z) - maxDistanceZ;
            float directionZ = Mathf.Sign(difference.z);

            playerA.position += new Vector3(0, 0, directionZ * excessZ * 0.5f);
            playerB.position -= new Vector3(0, 0, directionZ * excessZ * 0.5f);
        }
    }
}
