using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    float InteractionRange = 5f;
    [SerializeField]
    float RotationSpeed = 10f;

    //[SerializeField]
    //LayerMask PlayerLayer;

    [SerializeField]
    GameObject RewardObject;

    [SerializeField]
    Transform RewardsSpawnPoint;

    [SerializeField]
    List<KeyCode> InteractionKeyCodes;

    [SerializeField]
    Transform Model;

    [SerializeField]
    GameObject AvailableState;

    [SerializeField]
    GameObject ReadyToCompleteState;

    bool RewardSpawned = false;

    bool QuestObjectiveComplete = false;

    GameObject ClosestPlayer = null;

    public DialogueRunner dialogueRunner;

    public enum Animals
    {
        Deer,
        Squid,
        Monkey,
        Fish,
        Tavern
    }

    public Animals animal;

    // Update is called once per frame
    void Update()
    {
        QuestObjectiveComplete = CheckQuestConditions();
        SetQuestVisualStates();
        bool playerInRange = CheckForPlayerInRange();
        foreach (KeyCode keyCode in InteractionKeyCodes)
        {
            if (Input.GetKeyDown(keyCode) && playerInRange)
            {
                string questStarted;
                float questAmount;
                float questCount;

                var hasStartedQuest = dialogueRunner.VariableStorage.TryGetValue($"${animal}quest_status", out questStarted);
                var hasQuestAmount = dialogueRunner.VariableStorage.TryGetValue($"${animal}quest_amount", out questAmount);
                var hasQuestCount = dialogueRunner.VariableStorage.TryGetValue($"${animal}quest_count", out questCount);

                if (!hasQuestAmount || !hasQuestCount)
                {
                    QuestStartDiolog();
                }
                else if (hasQuestAmount && hasQuestCount)
                {
                    // We have prematurely completed the quest
                    if (questCount == questAmount)
                    {
                        QuestCompleteDiolog();
                    }

                    //we have started the quest
                    if (hasStartedQuest)
                    {
                        if (questStarted == "started" || questStarted == "completed")
                        {
                            QuestCompleteDiolog();
                        }

                        if (questStarted == "not_started")
                        {
                            QuestStartDiolog();
                        }
                    }
                    else
                    {
                        QuestStartDiolog();
                    }
                }

                break;
            }
        }
        if (playerInRange)
        {
            FacePlayer();
        }
    }

    public virtual void GiveReward()
    {
        if(RewardsSpawnPoint == null || RewardObject == null)
        {
            return;
        }
        Instantiate(RewardObject, RewardsSpawnPoint.position, RewardsSpawnPoint.rotation);
        Debug.Log("Reward Spawned");
        RewardSpawned = true;
    }

    bool CheckForPlayerInRange()
    {
        ClosestPlayer = null;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, InteractionRange);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.TryGetComponent<LockedCamPlayerMovement>(out var script))
            {
                ClosestPlayer = hitCollider.gameObject;
                return true;
            }
        }
        return false;
        //return Physics.CheckSphere(transform.position, InteractionRange, PlayerLayer);
    }

    void SetQuestVisualStates()
    {
        if(AvailableState == null || ReadyToCompleteState == null)
        {
            return;
        }
        if (RewardSpawned)
        {
            AvailableState.SetActive(false);
            ReadyToCompleteState.SetActive(false);
        }
        else if(QuestObjectiveComplete)
        {
            AvailableState.SetActive(false);
            ReadyToCompleteState.SetActive(true);
        }
        else if (!QuestObjectiveComplete)
        {
            AvailableState.SetActive(true);
            ReadyToCompleteState.SetActive(false);
        }

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

    virtual protected void QuestCompleteDiolog()
    {
        if (!dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.StartDialogue($"{animal}QuestComplete");
        }
    }

    virtual protected void QuestStartDiolog()
    {
        if (!dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.StartDialogue($"{animal}QuestStart");
        }
    }

    virtual protected bool CheckQuestConditions()
    {
        return true;
    }

    public virtual void OnQuestObjectiveDeactivated()
    {
        float questObjectivesCollected;
        dialogueRunner.VariableStorage.TryGetValue($"${animal}quest_amount", out questObjectivesCollected);
        dialogueRunner.VariableStorage.SetValue($"${animal}quest_amount", questObjectivesCollected + 1);
    }
}
