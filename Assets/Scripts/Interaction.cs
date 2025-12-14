using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionRaycast : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 3f) && context.performed)
        {
            Debug.Log("Interacted with " + hit.transform.name);
        }
    }

}
