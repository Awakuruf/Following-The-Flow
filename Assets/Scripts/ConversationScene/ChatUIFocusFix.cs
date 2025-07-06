using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ChatUIFocusFix : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private RAGBotManager chatManager;

    private void Start()
    {
        Debug.Log("[ChatUIFocusFix] Start() called");
        inputField.onSubmit.AddListener(HandleSubmit);
        StartCoroutine(DelayFocusInput());
    }

    private void HandleSubmit(string userInput)
    {
        Debug.Log("[ChatUI] onSubmit triggered. User input: " + userInput);
        if (!string.IsNullOrEmpty(userInput))
        {
            chatManager.AskRAGAi(userInput);
            Refocus();
        }
    }

    private void Update()
    {
        if (inputField == null || Keyboard.current == null)
        {
            Debug.LogError("[ChatUIFocusFix] inputField is NOT assigned!");
            return;

        }
        if (inputField.isFocused)
        {
            Debug.Log("[ChatUIFocusFix] Input field IS focused");
        }

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            Debug.Log("[ChatUIFocusFix] Enter key WAS pressed");
        }

        if (inputField.isFocused && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            Debug.Log("[ChatUIFocusFix] Input is focused AND Enter was pressed — calling SubmitMessage()");
            SubmitMessage();
        }

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("[ChatUIFocusFix] Escape key pressed — unlocking cursor");
            EventSystem.current.SetSelectedGameObject(null);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }



    private IEnumerator DelayFocusInput()
    {
        yield return new WaitForSeconds(0.1f);
        Refocus();
    }

    public void Refocus()
    {
        inputField.text = "";
        EventSystem.current.SetSelectedGameObject(null);
        inputField.Select();
        inputField.ActivateInputField();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void SubmitMessage()
    {
        string userInput = inputField.text.Trim();
        Debug.Log("[ChatUI] Enter pressed. User input: " + userInput);

        if (!string.IsNullOrEmpty(userInput))
        {
            Debug.Log("[ChatUI] Submitting to RAGBotManager...");
            chatManager.AskRAGAi(userInput);
            Refocus();
        }
    }

}
