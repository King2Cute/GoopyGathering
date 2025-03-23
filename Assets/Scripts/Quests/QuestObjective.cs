using UnityEngine;

public class QuestObjective : PickupItem
{
    public bool Collected = false;

    private QuestGiver questGiver;

    public void SetQuestGiver(QuestGiver qGiver)
    {
        questGiver = qGiver;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<LockedCamPlayerMovement>(out var script))
        {
            Collected = true;
            gameObject.SetActive(false);
            questGiver?.OnQuestObjectiveDeactivated();
        }
    }
}
