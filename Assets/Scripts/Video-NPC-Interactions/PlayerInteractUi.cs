using UnityEngine;
using TMPro;

public class PlayerInteractUi : MonoBehaviour
{
    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private TextMeshProUGUI interactTextMeshProUGUI;

    private void Update()
    {
        if (playerInteract.GetInteractableObject() != null)
        {
            Show(playerInteract.GetInteractableObject());
        }
        else
        {
            Hide();
        }
    }

    private void Show(NPCInteractable npcInteractable)
    {
        containerGameObject.SetActive(true);
        interactTextMeshProUGUI.text = npcInteractable.GetInteractText();
    }

    private void Hide()
    {
        containerGameObject.SetActive(false);
    }
}
