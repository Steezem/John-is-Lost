using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI speakerNameText;
    public TextMeshProUGUI dialogueText;
    public Button[] optionButtons;
    public GameObject dialoguePanel;

    public DialogueLine[] dialogueLines;
    private int currentLineIndex = 0;

    void Start()
    {
        dialoguePanel.SetActive(false);

        //Eventually load dialogue from a json file permaybe
    }

    public void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        currentLineIndex = 0;
        DisplayDialogueLine(dialogueLines[currentLineIndex]);
    }

    public void DisplayNextLine()
    {
        currentLineIndex++;
        if (currentLineIndex < dialogueLines.Length)
        {
            DisplayDialogueLine(dialogueLines[currentLineIndex]);
        } else
        {
            EndDialogue();
        }
    }

    private void DisplayDialogueLine(DialogueLine line)
    {
        speakerNameText.text = line.speakerName;
        dialogueText.text = line.text;

        if (line.options != null && line.options.Length > 0)
        {
            EnableOptions(line.options);
        } 
        else
        {
            DisableOptions();
        }
    }

    public void ChooseOption(int optionIndex)
    {
        DialogueOption chosenOption = dialogueLines[currentLineIndex].options[optionIndex];
        currentLineIndex = chosenOption.nextLineIndex;
        DisplayDialogueLine(dialogueLines[currentLineIndex]);
    }

    //If Dialogue has options to choose from - enable them
    private void EnableOptions(DialogueOption[] options)
    {
        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < options.Length)
            {
                int optionIndex = i;
                optionButtons[i].gameObject.SetActive(true);
                optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = options[i].text;
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => ChooseOption(optionIndex));
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void DisableOptions()
    {
        foreach(Button button in optionButtons)
        {
            button.gameObject.SetActive(false); 
        }
    }

    private void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}

[System.Serializable]
public class DialogueLine : MonoBehaviour
{
    public string speakerName;
    public string text;
    public DialogueOption[] options;

}

[System.Serializable]
public class DialogueOption : MonoBehaviour
{
    public string text;
    public int nextLineIndex;
}
