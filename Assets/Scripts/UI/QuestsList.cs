using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public GameObject questsTextPrefab;
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

        if (dayManager.dayCount == 1)
        {
            foreach (Quest quest in questsDay1)
            {
                GameObject questObj = Instantiate(questsTextPrefab, transform);
                TextMeshProUGUI questTMP = questObj.GetComponent<TextMeshProUGUI>();
                questTMP.text = "-> " + quest.questDescription;

                questTextByName[quest.questName] = questTMP;
            }
        }
        else if (dayManager.dayCount == 2)
        {
            foreach (Quest quest in questsDay2)
            {
                GameObject questObj = Instantiate(questsTextPrefab, transform);
                TextMeshProUGUI questTMP = questObj.GetComponent<TextMeshProUGUI>();
                questTMP.text = "-> " + quest.questDescription;

                questTextByName[quest.questName] = questTMP;
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
                StrikeThroughQuest("Play a game of Solitaire");
            }
        }

        
    }

    private void StrikeThroughQuest(string questName)
    {
        if (questTextByName.TryGetValue(questName, out TextMeshProUGUI questTMP))
        {
            if (!questTMP.text.Contains("<s>"))
            {
                questTMP.text = "<s>" + questTMP.text + "</s>";
                questTMP.color = new Color(questTMP.color.r, questTMP.color.g, questTMP.color.b, 0.5f);
            }
        }
    }
}
