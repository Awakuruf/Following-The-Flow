using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainScene : MonoBehaviour
{
    public void GoBack()
    {
        SceneManager.LoadScene("Daoism-VillageScene");
    }
}