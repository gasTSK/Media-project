using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This script controls the dialogue box UI.
// Attach it to an empty GameObject called "DialogueManager" in your scene.
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance; // lets any script access this easily

    [Header("UI Elements")]
    public GameObject dialoguePanel;       // the dialogue box panel
    public TextMeshProUGUI nameText;       // NPC name at the top
    public TextMeshProUGUI dialogueText;   // the line of dialogue
    public Button nextButton;             // the "Next" / "Close" button
    public TextMeshProUGUI nextButtonText; // text on that button

    private string[] currentLines;  // all lines for the current conversation
    private int currentIndex = 0;   // which line we're on
    private bool isTyping = false;  // are we mid-typing animation?

    void Awake()
    {
        // Singleton: only one DialogueManager can exist
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        dialoguePanel.SetActive(false); // hide on start
    }

    // Call this from any NPC script to start a conversation
    public void StartDialogue(string npcName, string[] lines)
    {
        currentLines = lines;
        currentIndex = 0;
        nameText.text = npcName;
        dialoguePanel.SetActive(true);
        ShowLine();
    }

    // Called when player clicks Next button
    public void OnNextButtonClicked()
    {
        if (isTyping)
        {
            // If still typing, skip to full line instantly
            StopAllCoroutines();
            dialogueText.text = currentLines[currentIndex];
            isTyping = false;
            UpdateButtonLabel();
            return;
        }

        currentIndex++;

        if (currentIndex < currentLines.Length)
        {
            ShowLine(); // show next line
        }
        else
        {
            EndDialogue(); // no more lines, close box
        }
    }

    void ShowLine()
    {
        UpdateButtonLabel();
        StartCoroutine(TypeLine(currentLines[currentIndex]));
    }

    // Typing animation — reveals text letter by letter
    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(0.03f); // speed of typing
        }

        isTyping = false;
        UpdateButtonLabel();
    }

    void UpdateButtonLabel()
    {
        bool isLastLine = currentIndex >= currentLines.Length - 1;
        nextButtonText.text = (isLastLine && !isTyping) ? "Close" : "Next";
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        currentLines = null;
        currentIndex = 0;
    }
}
