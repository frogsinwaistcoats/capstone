using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using System.Collections.Generic;

public class PortraitLineView : LineView
{
    [Header("Portrait Settings")]
    [SerializeField] private Image leftPortrait;
    [SerializeField] private Image rightPortrait;
    [SerializeField] private float inactiveScale = 0.9f;

    [Header("Character Sprites")]
    [SerializeField] private List<CharacterPortrait> portraits;

    private Dictionary<string, Sprite> portraitLookup = new Dictionary<string, Sprite>();

    [System.Serializable]

    public class CharacterPortrait
    {
        public string characterName;
        public Sprite sprite;
        public bool isRightSide;
    }

    protected void Start()
    {
        if (leftPortrait == null) Debug.LogError("Left portrait not assigned!");
        if (rightPortrait == null) Debug.LogError("Right portrait not assigned!");
        if (canvasGroup == null) Debug.LogError("CanvasGroup not found!");

        foreach (var p in portraits)
        {
            if (!portraitLookup.ContainsKey(p.characterName))
            {
                portraitLookup.Add(p.characterName, p.sprite);
            }
        }
    }

    public override void RunLine(LocalizedLine dialogueLine, System.Action onFinished)
    {
        base.RunLine(dialogueLine, onFinished);

        string speakerName = dialogueLine.CharacterName;

        UpdatePortraits(speakerName);
    }

    private void UpdatePortraits(string speakerName)
    {
        CharacterPortrait speakerPortrait = portraits.Find(p => p.characterName == speakerName);

        if (speakerPortrait == null)
        {
            Debug.LogWarning($"No portrait found for character: {speakerName}");
            return;
        }

        if (speakerPortrait.isRightSide)
        {
            rightPortrait.sprite = speakerPortrait.sprite;
            setPortraitActive(rightPortrait, true);
            setPortraitActive(leftPortrait, false);
        }
        else
        {
            leftPortrait.sprite = speakerPortrait.sprite;
            setPortraitActive(leftPortrait, true);
            setPortraitActive(rightPortrait, false);
        }
    }

    private void setPortraitActive(Image img, bool isActive)
    {
        if (isActive)
        {
            img.color = Color.white;
            img.transform.localScale = Vector3.one;
        }
        else
        {
            img.color = new Color(0.6f, 0.6f, 0.6f, 1f);
            img.transform.localScale = Vector3.one * inactiveScale;
        }
    }

    public void DimLeftPortrait()
    {
        leftPortrait.color = new Color(0.6f, 0.6f, 0.6f, 1f);
        rightPortrait.color = Color.white;
    }

    public void ResetPortraits()
    {
        leftPortrait.color = Color.white;
        rightPortrait.color = new Color(0.6f, 0.6f, 0.6f, 1f);
    }
}
