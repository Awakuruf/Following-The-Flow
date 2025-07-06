using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Deprecated Class
public class ChatManager : MonoBehaviour
{
    public GameObject chatPanel;
    public TMP_InputField inputField;
    public TextMeshProUGUI chatContent;
    public ScrollRect scrollRect;

    public void ShowChat()
    {
        chatPanel.SetActive(true);
        inputField.Select();
    }

    public void HideChat()
    {
        chatPanel.SetActive(false);
    }

    public void OnSendMessage()
    {
        string playerMessage = inputField.text;
        if (string.IsNullOrWhiteSpace(playerMessage)) return;

        AppendMessage("You: " + playerMessage);
        inputField.text = "";
        inputField.ActivateInputField();

        AppendMessage("NPC: I'm listening...");
    }

    private void AppendMessage(string message)
    {
        chatContent.text += message + "\n";
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}
