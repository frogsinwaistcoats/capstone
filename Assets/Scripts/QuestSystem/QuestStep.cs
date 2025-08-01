using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;

    protected void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;

            // TO DO - advance the quest forward now that weve finished this step

            Destroy(this.gameObject);
        }
    }
}
