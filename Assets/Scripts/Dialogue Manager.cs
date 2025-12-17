using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject playerCharacter;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Button[] optionButtons;
    public GameObject dialoguePanel;

    public Animator animator;
    private Queue<string> sentences;
    private bool inDialogue = false;

    void Start()
    {
        sentences = new Queue<string>();
        //dialoguePanel.SetActive(false);

        //Eventually load dialogue from a json file permaybe
    }

    public void StartDialogue(Dialogue dialogue)
    {
        // this catches the Interaction call of StartDialogue
        // It works, but is very spaghetti-like
        //if (inDialogue) return;

        nameText.text = dialogue.name;
        sentences.Clear();
        playerCharacter.GetComponent<CharacterMovement>().enabled = false;
        playerCharacter.GetComponent<InteractionRaycast>().enabled = false;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        //dialoguePanel.SetActive(true);
        animator.SetBool("isOpen", true);
        inDialogue = true;


        
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        Debug.Log("EndDialogue fires");
        animator.SetBool("isOpen", false);
        //dialoguePanel.SetActive(false);
        playerCharacter.GetComponent<CharacterMovement>().enabled = true;
        playerCharacter.GetComponent<InteractionRaycast>().enabled = true;
        inDialogue = false;
    }

    public void OnInteractInConvo(InputAction.CallbackContext context)
    {
        // If a button action has to be registered always check for only context.started, context.performed or context.canceled.
        // Else all three actions will register and the button will have "been pressed" thrice
        if (inDialogue && context.performed)
        {
            DisplayNextSentence();
        }

    }

}
