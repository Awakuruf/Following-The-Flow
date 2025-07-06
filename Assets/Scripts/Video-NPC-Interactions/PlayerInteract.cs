using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic; 


public class PlayerInteract : MonoBehaviour
{
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Interact.performed += OnInteractPressed;
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Interact.performed -= OnInteractPressed;
        inputActions.Player.Disable();
    }

    private void OnInteractPressed(InputAction.CallbackContext context)
    {
        float interactRange = 2f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NPCInteractable npcInteractable))
            {
                npcInteractable.Interact();
            }
        }
    }

    public NPCInteractable GetInteractableObject()
    {
        List<NPCInteractable> npcInteractableList = new List<NPCInteractable>();

        float interactRange = 4f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NPCInteractable npcInteractable))
            {
                npcInteractableList.Add(npcInteractable);
            }
        }

        NPCInteractable closestNPCInteractable = null;
        foreach (NPCInteractable npcInteractable in npcInteractableList)
        {
            if (closestNPCInteractable == null)
            {
                closestNPCInteractable = npcInteractable;
            }
            else
            {
                if (Vector3.Distance(transform.position, npcInteractable.transform.position) <
                    Vector3.Distance(transform.position, closestNPCInteractable.transform.position))
                {
                    // Closer
                    closestNPCInteractable = npcInteractable;
                }
            }
        }
        return closestNPCInteractable;
    }
}
