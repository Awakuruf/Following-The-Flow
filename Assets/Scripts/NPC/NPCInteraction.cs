using UnityEngine;
using UnityEngine.InputSystem;

// Deprecated Class

public class NPCInteraction : MonoBehaviour
{
    private bool isNearNPC = false;

    [Header("UI Elements")]
    public GameObject promptUI;
    public ChatManager chatManager;

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
        if (isNearNPC)
        {
            Debug.Log("Opening chat!");
            promptUI.SetActive(false);
            chatManager.ShowChat();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered trigger with: " + other.name);
        if (other.CompareTag("NPC"))
        {
            isNearNPC = true;
            promptUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            isNearNPC = false;
            promptUI.SetActive(false);
        }
    }
}
