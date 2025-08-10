using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;

    private void Awake()
    {
        questMap = CreateQuestMap();
    }

    private void OnEnable()
    {
        //GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        //GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        //GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;
    }

    private void OnDisable()
    {
        //GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        //GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        //GameEventsManager.instance.questEvents.onFinishQuest -= FinishQuest;
    }

    private void StartQuest(string id)
    {
        // TODO - start the quest
        Debug.Log("Start quest: " + id);
    }

    private void AdvanceQuest(string id)
    {
        // TODO - advance the quest
        Debug.Log("Advance quest: " + id);
    }

    private void FinishQuest(string id)
    {
        // TODO - finish the quest
        Debug.Log("Finish quest: " + id);
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        // loads all QuestInfoSO scriptable objects under the assets/resources/quests folder
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");
        // create the quest map
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfoSO questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning("Duplicate ID found when creating quest map: " + questInfo.id);
            }
            idToQuestMap.Add(questInfo.id, new Quest(questInfo));
        }
        return idToQuestMap;
    }

    private Quest GetQuestById(string id)
    {
        Quest quest = questMap[id];
        if (quest == null)
        {
            Debug.LogError("ID not found in the quest map: " + id);
        }
        return quest;
    }
}
