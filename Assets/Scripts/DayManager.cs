using UnityEngine;
using TMPro;
using Yarn.Unity;

public class DayManager : MonoBehaviour, IDataPersistence
{
    public int dayCount { get; private set; } = 1;
    public TextMeshProUGUI dayCounterText;
    private DialogueRunner dialogueRunner;


    public void LoadData(GameData data)
    {
        this.dayCount = data.dayCount;
    }

    public void SaveData(GameData data)
    {
        data.dayCount = this.dayCount;
    }

    private void Start()
    {
        dialogueRunner = FindFirstObjectByType<DialogueRunner>();

        if(dayCounterText == null)
        {
            GameObject textObject = GameObject.Find("---- UI ----/OtherCanvas/DayCounterText");
            if(textObject != null)
            {
                dayCounterText = textObject.GetComponent<TextMeshProUGUI>();
            }
        }

        UpdateDayText();
    }

    public void StartNewDay()
    {
        dayCount++;
        UpdateDayText();

        //set yarn variable
        if (dialogueRunner != null)
        {
            dialogueRunner.VariableStorage.SetValue("$day", dayCount);
        }
    }

    public void UpdateDayText()
    {
        if (dayCounterText != null)
        {
            dayCounterText.text = "Day " + dayCount.ToString();
        }
    
    }
}
