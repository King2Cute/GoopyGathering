using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("TheLevel");
    }

    public void StartMultiplayerLobby()
    {
        SceneManager.LoadScene("MultiplayerLobby");
    }
}
