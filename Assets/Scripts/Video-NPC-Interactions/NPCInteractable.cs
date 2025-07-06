using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCInteractable : MonoBehaviour
{
    [SerializeField] private string interactText;
    public void Interact()
    {
        SaveData.savedPlayerPosition = GameObject.FindWithTag("Player").transform.position;
        Debug.Log("Saved position: " + SaveData.savedPlayerPosition);
        SceneManager.LoadScene("ConversationScene");
    }
    public string GetInteractText()
    {
        return interactText;
    }
}
