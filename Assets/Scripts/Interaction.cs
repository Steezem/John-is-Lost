using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionRaycast : MonoBehaviour
{
    public void OnInteract(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 3f) && context.performed && gameObject.GetComponent<InteractionRaycast>().isActiveAndEnabled)
        {
            //Debug.Log("Interacted with " + hit.transform.name + hit.transform.tag);
            if (hit.transform.tag == "NPC")
            {
                // by doing this, you don't have to manually add the DialogueManager to the Interaction Script on the player character
                // Problems may arise if multiple Dialogue Managers are available
                FindAnyObjectByType<DialogueManager>().StartDialogue(hit.transform.GetComponent<DialogueTrigger>().dialogue);
            }
        }
    }

}
