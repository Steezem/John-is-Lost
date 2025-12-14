using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionRaycast : MonoBehaviour
{
    public void OnInteract(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 3f) && context.performed)
        {
            Debug.Log("Interacted with " + hit.transform.name);
        }
    }

}
