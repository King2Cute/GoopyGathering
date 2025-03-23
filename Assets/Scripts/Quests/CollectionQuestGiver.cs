using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CollectionQuestGiver : QuestGiver
{
    [SerializeField]
    List<QuestObjective> QuestObjectives;

    [SerializeField]
    GameObject QuestObjectiveHolder;

    [SerializeField]
    bool SuckQuest = false;
    [SerializeField]
    int SucksNeeded = 10;
    [SerializeField]
    int KeysNeeded = 4;

    private void Start()
    {
        if (QuestObjectiveHolder != null && QuestObjectives != null)
        {
            QuestObjectives.AddRange(QuestObjectiveHolder.GetComponentsInChildren<QuestObjective>());
        }

        foreach (var obj in QuestObjectives)
        {
            obj.SetQuestGiver(this);
        }

        SetQuestAmount();
    }

    bool subscribed = false;
    void LateUpdate()
    {
        if (!subscribed)
        {
            MuliplayerController.instance.SlimeEaten += SlimeEaten;
            MuliplayerController.instance.KeyCollected += KeyCollected;
        }
    }
    void SlimeEaten()
    {
        if (SuckQuest)
        {
            OnQuestObjectiveDeactivated();
        }
    }
    void KeyCollected()
    {
        if(animal == Animals.Tavern)
        {
            OnQuestObjectiveDeactivated();
        }
    }

    protected override bool CheckQuestConditions()
    {
        float questObjectivesCollected;
        var hasQuestObectiveColleted = dialogueRunner.VariableStorage.TryGetValue($"${animal}quest_amount", out questObjectivesCollected);

        if (hasQuestObectiveColleted)
        {
            int needed = 0;
            if(animal == Animals.Tavern)
            {
                needed = KeysNeeded;
            }
            else if(SuckQuest)
            {
                needed = SucksNeeded;
            }
            else
            {
                needed = QuestObjectives.Count;
            }

            if ((int)questObjectivesCollected >= needed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    [YarnCommand("give_reward")]
    public override void GiveReward()
    {
        base.GiveReward();
        Debug.Log("Reward for collection quest given!");
    }

    private void SetQuestAmount()
    {
        if(animal == Animals.Tavern)
        {

            dialogueRunner.VariableStorage.SetValue($"${animal}quest_count", KeysNeeded);
        }
        if (SuckQuest)
        {
            dialogueRunner.VariableStorage.SetValue($"${animal}quest_count", SucksNeeded);
        }
        else
        {
            dialogueRunner.VariableStorage.SetValue($"${animal}quest_count", QuestObjectives.Count);
        }
    }

    protected override void QuestCompleteDiolog()
    {
        if (QuestObjectives.Count > 0)
        {
            MuliplayerController.instance.QuestCompleted(QuestObjectives[0].type);
        }
        if (!dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.StartDialogue($"{animal}QuestComplete");
        }
    }
}
