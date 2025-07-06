using UnityEngine;
using System.Collections;

public class PlayerPositionRestorer : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(RestoreDelayed());
    }

    private IEnumerator RestoreDelayed()
    {
        yield return new WaitForEndOfFrame(); 
        if (SaveData.savedPlayerPosition != Vector3.zero)
        {
            transform.position = SaveData.savedPlayerPosition;
            Debug.Log("Restored player position (delayed): " + transform.position);
        }
    }
}
