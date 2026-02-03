using UnityEngine;
using Ink.Runtime;
using TMPro;

public class TutorialTextTrigger : MonoBehaviour
{
    public TextAsset inkJSON;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    private Story currentStory;

    void Start()
    {
        if(dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        if(inkJSON != null)
        {
            currentStory = new Story(inkJSON.text);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            ShowDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            HideDialogue();
        }
    }

    private void ShowDialogue()
    {
        if(currentStory == null || dialoguePanel == null || dialogueText == null)
        {
            return;
        }

        currentStory.ResetState();
        dialoguePanel.SetActive(true);

        string fullText = "";
        while(currentStory.canContinue)
        {
            fullText += currentStory.Continue();
        }
        dialogueText.text = fullText.Trim();
    }

    private void HideDialogue()
    {
        if(dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }
}
