using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestsList : MonoBehaviour
{

    [Header("Quests - Day 1")]
    [SerializeField] private List<Quest> questsDay1;

    [Header("Quests - Day 2")]
    [SerializeField] private List<Quest> questsDay2;

    [Header("Quests - Day 3")]
    [SerializeField] private List<Quest> questsDay3;

    [Header("Quests - Day 4")]
    [SerializeField] private List<Quest> questsDay4;

    [Header("Quests - Day 5")]
    [SerializeField] private List<Quest> questsDay5;

    [Header("Quests - Day 6")]
    [SerializeField] private List<Quest> questsDay6;

    [Header("Quests - Day 7")]
    [SerializeField] private List<Quest> questsDay7;

    private List<GameObject> activeQuestTexts = new List<GameObject>();

    public GameObject questsTextPrefab;
    public GameObject questsVisualHolder;
    DayManager dayManager;

    private Dictionary<string, TextMeshProUGUI> questTextByName = new Dictionary<string, TextMeshProUGUI>();

    [System.Serializable]
    public class Quest
    {
        public string questName;
        [TextArea]
        public string questDescription;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (dayManager == null)
        {
            dayManager = FindFirstObjectByType<DayManager>();
        }

        StartCoroutine(StartQuestsNextFrame());
    }

    public IEnumerator StartQuestsNextFrame()
    {
        yield return null; 
        StartNewQuests();
    }

    public void StartNewQuests()
    {

        Debug.Log("Active quest texts before clear: " + activeQuestTexts.Count);
        if (activeQuestTexts.Count > 0)
        {
            foreach (GameObject questObj in activeQuestTexts)
            {
                Destroy(questObj);
            }
            activeQuestTexts.Clear();
            questTextByName.Clear();
        }

        if (DayManager.instance.dayCount == 1)
        {
            foreach (Quest quest in questsDay1)
            {
                AddQuestToList(quest);
            }
        }
        else if (DayManager.instance.dayCount == 2)
        {
            foreach (Quest quest in questsDay2)
            {
                AddQuestToList(quest);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dayManager.dayCount == 1)
        {
            if (LoadYarnVariables.instance.GetInt("$peopleMet") >= 9)
            {
                StrikeThroughQuest("Meet everyone");
            }

            if (LoadYarnVariables.instance.GetBool("$hasUnpacked"))
            {
                StrikeThroughQuest("Unpack your bag");
            }

            if (LoadYarnVariables.instance.GetBool("$campfireStoryRead"))
            {
                StrikeThroughQuest("Campfire_Day1");
            }
        }
        else if (dayManager.dayCount == 2)
        {
            if (GameManager.instance.hasPlayedSolitaire)
            {
                StrikeThroughQuest("Play Solitaire");
            }
            if (LoadYarnVariables.instance.GetBool("$triggerSneakOut"))
            {
                
            }
            if (CampfireInteractable.instance.hasInteractedTwo)
            {
                StrikeThroughQuest("Campfire_Day2");
                AddQuestToList(new Quest { questName = "Sneak out", questDescription = "Sneak out of camp at night" });
                CampBorder.instance.EnableTriggerCollider();
            }
        }
        else if (dayManager.dayCount == 3)
        {
            if (LoadYarnVariables.instance.GetBool("$hasFished"))
            {
                StrikeThroughQuest("Fishing");
            }
            if (LoadYarnVariables.instance.GetBool("hasCooked"))
            {
                StrikeThroughQuest("Cooking");
            }
            if (CampfireInteractable.instance.hasInteractedThree)
            {
                StrikeThroughQuest("Campfire_Day3");
                AddQuestToList(new Quest { questName = "Sneak out", questDescription = "Sneak out of camp at night" });
                CampBorder.instance.EnableTriggerCollider();
            }
        }

        
    }

    public void AddQuestToList(Quest quest)
    {
        GameObject questObj = Instantiate(questsTextPrefab, questsVisualHolder.transform);
        activeQuestTexts.Add(questObj);
        TextMeshProUGUI questTMP = questObj.GetComponent<TextMeshProUGUI>();
        questTMP.text = "-> " + quest.questDescription;

        questTextByName[quest.questName] = questTMP;
    }

    private void StrikeThroughQuest(string questName)
    {
        if (questTextByName.TryGetValue(questName, out TextMeshProUGUI questTMP))
        {
            if (!questTMP.text.Contains("<s>"))
            {
                AudioManager.instance.Play("QuestComplete");
                questTMP.text = "<s>" + questTMP.text + "</s>";
                questTMP.color = new Color(questTMP.color.r, questTMP.color.g, questTMP.color.b, 0.5f);
            }
        }
    }
}
