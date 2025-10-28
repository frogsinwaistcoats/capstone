using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestsList : MonoBehaviour
{
    public static QuestsList instance;

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

    private List<GameObject> activeQuestTexts = new List<GameObject>();

    public GameObject questsTextPrefab;
    public GameObject questsVisualHolder;
    DayManager dayManager;

    public GameObject bus;
    public GameObject ruby;


    [SerializeField] private bool addedSneakOutQuestDay2 = false;
    [SerializeField] private bool addedSneakOutQuestDay3 = false;


    [SerializeField] private bool addedCampfireQuestDay4 = false;

    [SerializeField] private bool addedCameraQuest = false;
    [SerializeField] private bool addedReturnToWilsonQuest = false;

    [SerializeField] private bool addedLeaveCampQuest = false;

    [SerializeField] private bool addedSleepQuestDay1 = false;
    [SerializeField] private bool addedSleepQuestDay2 = false;
    [SerializeField] private bool addedSleepQuestDay3 = false;
    [SerializeField] private bool addedSleepQuestDay4 = false;

    private Dictionary<string, TextMeshProUGUI> questTextByName = new Dictionary<string, TextMeshProUGUI>();

    [System.Serializable]
    public class Quest
    {
        public string questName;
        [TextArea]
        public string questDescription;
    }

    private void Awake()
    {
        instance = this;
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
        else if (DayManager.instance.dayCount == 3)
        {
            foreach (Quest quest in questsDay3)
            {
                AddQuestToList(quest);
            }
        }
        else if (DayManager.instance.dayCount == 4)
        {
            foreach (Quest quest in questsDay4)
            {
                AddQuestToList(quest);
            }
        }
        else if (DayManager.instance.dayCount == 5)
        {
            foreach (Quest quest in questsDay5)
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

                if (!addedSleepQuestDay1)
                {
                    Quest sleep = (new Quest
                    {
                        questName = "Sleep_Day1",
                        questDescription = "Go to sleep"
                    });
                    questsDay1.Add(sleep);
                    AddQuestToList(sleep);
                    addedSleepQuestDay1 = true;
                }

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
            if (LoadYarnVariables.instance.GetBool("$campfireDay2"))
            {
                StrikeThroughQuest("Campfire_Day2");

                if (!addedSneakOutQuestDay2)
                {
                    Quest sneak = (new Quest
                    {
                        questName = "SneakOutDay2",
                        questDescription = "Sneak out using the path on the left"
                    });
                    questsDay2.Add(sneak);
                    AddQuestToList(sneak);
                    addedSneakOutQuestDay2 = true;
                    
                }
                CampBorder.instance.CanEnter();
            }

            if (GameManager.instance.hasSnuckOutDay2)
            {
                StrikeThroughQuest("SneakOutDay2");
                //GameManager.instance.hasSnuckOut = false;

                if (!addedSleepQuestDay2)
                {
                    Quest sleep = (new Quest
                    {
                        questName = "Sleep_Day2",
                        questDescription = "Go to sleep"
                    });
                    questsDay2.Add(sleep);
                    AddQuestToList(sleep);
                    addedSleepQuestDay2 = true;
                }
            }

        }
        else if (dayManager.dayCount == 3)
        {
            if (LoadYarnVariables.instance.GetBool("$hasFished"))
            {
                StrikeThroughQuest("Fishing");
            }
            if (LoadYarnVariables.instance.GetBool("$hasCooked"))
            {
                StrikeThroughQuest("Cooking");
            }
            if (LoadYarnVariables.instance.GetBool("$campfireDay3"))
            {
                StrikeThroughQuest("Campfire_Day3");
                if (!addedSneakOutQuestDay3)
                {
                    Quest sneak = (new Quest
                    {
                        questName = "Sneak out day3",
                        questDescription = "Sneak out to see Alex"
                    });
                    questsDay3.Add(sneak);
                    AddQuestToList(sneak);
                    addedSneakOutQuestDay3 = true;
                }
                CampBorder.instance.CanEnter();
            }

            if (GameManager.instance.hasSnuckOutDay3)
            {
                StrikeThroughQuest("Sneak out day3");

                if (!addedSleepQuestDay3)
                {
                    Quest sleep = (new Quest
                    {
                        questName = "Sleep_Day3",
                        questDescription = "Go to sleep"
                    });
                    questsDay3.Add(sleep);
                    AddQuestToList(sleep);
                    addedSleepQuestDay3 = true;
                }
            }
        }
        else if (dayManager.dayCount == 4)
        {
            if (LoadYarnVariables.instance.GetBool("$canUseCamera"))
            {
                StrikeThroughQuest("Talk To Mr Wilson");

                if (!addedCameraQuest)
                {
                    Quest flowers = (new Quest
                    {
                        questName = "Take photos of flowers",
                        questDescription = "Take photos of five native plants around the camp"
                    });
                    questsDay4.Add(flowers);
                    AddQuestToList(flowers);
                    addedCameraQuest = true;
                }
                
            }
            if (LoadYarnVariables.instance.GetBool("$hasGotFlowers"))
            {
                StrikeThroughQuest("Take photos of flowers");
                if (!addedReturnToWilsonQuest)
                {
                    Quest returnQuest = (new Quest
                    {
                        questName = "Return to Mr Wilson",
                        questDescription = "Go back to Mr Wilson"
                    });
                    questsDay4.Add(returnQuest);
                    AddQuestToList(returnQuest);
                    addedReturnToWilsonQuest = true;
                }
            }
            if (LoadYarnVariables.instance.GetBool("$talkAfterFlowers"))
            {
                StrikeThroughQuest("Return to Mr Wilson");
                if (!addedCampfireQuestDay4)
                {
                    Quest campfire = (new Quest
                    {
                        questName = "Campfire_Day4",
                        questDescription = "Go to the campfire"
                    });
                    questsDay4.Add(campfire);
                    AddQuestToList(campfire);
                    addedCampfireQuestDay4 = true;
                }
                CampBorder.instance.CanEnter();
            }
            if (LoadYarnVariables.instance.GetBool("$campfireDay4"))
            {
                StrikeThroughQuest("Campfire_Day4");

                if (!addedSleepQuestDay4)
                {
                    Quest sleep = (new Quest
                    {
                        questName = "Sleep_Day4",
                        questDescription = "Go to sleep"
                    });
                    questsDay4.Add(sleep);
                    AddQuestToList(sleep);
                    addedSleepQuestDay4 = true;
                }
            }
        }
        else if (DayManager.instance.dayCount == 5)
        {
            if (GameManager.instance.hasSnuckOutDay5)
            {
                StrikeThroughQuest("Sneak Out day5");

                if (!addedLeaveCampQuest)
                {
                    Quest leave = (new Quest
                    {
                        questName = "Leave",
                        questDescription = "Get on the bus to leave camp"
                    });
                    questsDay5.Add(leave);
                    AddQuestToList(leave);
                    addedLeaveCampQuest = true;
                    bus.SetActive(true);
                    ruby.transform.position = new Vector3(-11.9700003f, -0.0799999982f, -20.8199997f);
                }
            }
        }
    }

    public void AddQuestToList(Quest quest)
    {
        InventoryManager.instance.newQuestsToSee = true;
        GameObject questObj = Instantiate(questsTextPrefab, questsVisualHolder.transform);
        activeQuestTexts.Add(questObj);
        TextMeshProUGUI questTMP = questObj.GetComponent<TextMeshProUGUI>();
        questTMP.text = "-> " + quest.questDescription;

        questTextByName[quest.questName] = questTMP;
    }

    public void StrikeThroughQuest(string questName)
    {
        if (!questTextByName.TryGetValue(questName, out TextMeshProUGUI questTMP))
        {
            Debug.LogWarning($"Quest '{questName}' not found in dictionary — trying again next frame.");
            StartCoroutine(StrikeNextFrame(questName));
            return;
        }

        if (!questTMP.text.Contains("<s>"))
        {
            questTMP.text = "<s>" + questTMP.text + "</s>";
            questTMP.color = new Color(questTMP.color.r, questTMP.color.g, questTMP.color.b, 0.5f);
            if (InventoryManager.instance != null)
                InventoryManager.instance.newQuestsToSee = true;
        }
    }

    private IEnumerator StrikeNextFrame(string questName)
    {
        yield return null;
        StrikeThroughQuest(questName);
    }
}
