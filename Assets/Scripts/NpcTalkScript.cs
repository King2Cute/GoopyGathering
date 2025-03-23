using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class NpcTalkScript : MonoBehaviour
{
    [SerializeField]
    float InteractionRange = 5f;

    [SerializeField]
    float RotationSpeed = 10f;

    [SerializeField]
    List<KeyCode> InteractionKeyCodes;

    [SerializeField]
    Transform Model;

    public string dialogName;

    DialogueRunner dialogueRunner;

    GameObject ClosestPlayer = null;

    private void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>(); 
    }

    private void Update()
    {
        bool playerInRange = CheckForPlayerInRange();
        foreach (KeyCode keyCode in InteractionKeyCodes)
        {
            if (Input.GetKeyDown(keyCode) && playerInRange)
            {
                StartDialog();
            }
        }

        if (playerInRange)
        {
            FacePlayer();
        }
    }

    private void StartDialog()
    {
        if (!dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.StartDialogue(dialogName);
        }
    }

    bool CheckForPlayerInRange()
    {
        ClosestPlayer = null;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, InteractionRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.TryGetComponent<LockedCamPlayerMovement>(out var script))
            {
                ClosestPlayer = hitCollider.gameObject;
                return true;
            }
        }
        return false;
    }

    void FacePlayer()
    {
        if (Model == null)
        {
            return;
        }
        if (ClosestPlayer == null)
        {
            Model.rotation = Quaternion.Lerp(
                Model.rotation, Quaternion.LookRotation(transform.forward, Vector3.up),
                Time.deltaTime * RotationSpeed);
            return;
        }
        else
        {
            Vector3 lookDirection = ClosestPlayer.transform.position - transform.position;
            lookDirection.y = 0;//transform.position.y;
            Model.rotation = Quaternion.Lerp(
                Model.rotation, Quaternion.LookRotation(lookDirection, Vector3.up),
                Time.deltaTime * RotationSpeed);
        }
    }
}
