using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class DialogueBackgroundManager : MonoBehaviour
{
    public static DialogueBackgroundManager instance;

    [SerializeField] private Image backgroundImage;
    [SerializeField] private List<DialogueBackground> backgrounds;

    private Dictionary<string, Sprite> backgroundLookup = new Dictionary<string, Sprite>();
    private DialogueRunner dialogueRunner;

    [System.Serializable]
    public class DialogueBackground
    {
        public string tagName;
        public Sprite backgroundSprite;
    }

    private void Awake()
    {
        instance = this;

        foreach (var bg in backgrounds)
        {
            if (!backgroundLookup.ContainsKey(bg.tagName))
                backgroundLookup.Add(bg.tagName, bg.backgroundSprite);
        }

        dialogueRunner = FindFirstObjectByType<DialogueRunner>();
        if (dialogueRunner != null)
        {
            dialogueRunner.onNodeStart.AddListener(OnNodeStart);
        }
    }

    private void OnNodeStart(string nodeName)
    {
        // Hide current background by default
        backgroundImage.enabled = false;

        if (dialogueRunner == null)
            return;

        // Get any tags attached to this node (via `tags:` line in Yarn)
        var tags = dialogueRunner.GetTagsForNode(nodeName);

        if (tags == null || !tags.Any())
            return;

        foreach (var tag in tags)
        {
            if (backgroundLookup.TryGetValue(tag, out Sprite sprite))
            {
                backgroundImage.sprite = sprite;
                backgroundImage.enabled = true;
                return;
            }
        }
    }

    [YarnCommand("setBackground")]
    public void SetBackground(string tag)
    {
        // Optional Yarn command if you prefer manual control
        if (backgroundLookup.TryGetValue(tag, out Sprite sprite))
        {
            backgroundImage.sprite = sprite;
            backgroundImage.enabled = true;
        }
    }

    [YarnCommand("clearBackground")]
    public void ClearBackground()
    {
        backgroundImage.enabled = false;
    }

    public void PlayRevealAnim()
    {
        GetComponent<Animator>().Play("Reveal_Anim");
    }

    public void DisableAnimator()
    {
        GetComponent<Animator>().enabled = false;
        ClearBackground();
    }
}
