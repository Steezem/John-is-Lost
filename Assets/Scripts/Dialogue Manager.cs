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
        playerCharacter.GetComponent<CharacterMovement>().enabled = false;
        dialoguePanel.SetActive(true);
        currentLineIndex = 0;
        inDialogue = true;
        Debug.Log("Starting conversation with " + dialogue.name);
        playerCharacter.GetComponent<CharacterMovement>().enabled = true;
        dialoguePanel.SetActive(false);
    }

    public void OnInteractInConvo(InputAction.CallbackContext context)
    {
        Debug.Log("Yes it gets recognized");
    }

}
