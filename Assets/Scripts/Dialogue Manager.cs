using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject playerCharacter;
    public TextMeshProUGUI speakerNameText;
    public Button[] optionButtons;
    public GameObject dialoguePanel;

    private int currentLineIndex = 0;
    private Queue<string> sentences;
    private bool inDialogue = false;

    void Start()
    {
        sentences = new Queue<string>();
        dialoguePanel.SetActive(false);

        //Eventually load dialogue from a json file permaybe
    }

    public void StartDialogue(Dialogue dialogue)
    {
        // this catches the Interaction call of StartDialogue
        // It works, but is very spaghetti-like
        // Another found solution is below - disabling the InteractionRaycast Module of the Player Character until EndDialogue is called
        if (inDialogue) return;
        sentences.Clear();
        playerCharacter.GetComponent<CharacterMovement>().enabled = false;
        //playerCharacter.GetComponent<InteractionRaycast>().enabled = false;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        dialoguePanel.SetActive(true);
        inDialogue = true;
        Debug.Log("Starting conversation with " + dialogue.name);


        
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        Debug.Log(sentence);


    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        playerCharacter.GetComponent<CharacterMovement>().enabled = true;
        playerCharacter.GetComponent<InteractionRaycast>().enabled = true;
        inDialogue = false;
        Debug.Log("End of Conversation");
    }

    public void OnInteractInConvo(InputAction.CallbackContext context)
    {
        // If a button action has to be registered always check for only context.started, context.performed or context.canceled.
        // Else all three actions will register and the button will have "been pressed" thrice
        if (inDialogue && context.performed)
        {
            Debug.Log("Yes it gets recognized");
            DisplayNextSentence();
        }

    }

}
