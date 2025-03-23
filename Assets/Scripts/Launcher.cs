using UnityEngine;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("TheLevel", LoadSceneMode.Additive);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}