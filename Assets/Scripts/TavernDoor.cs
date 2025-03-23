using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TavernDoor : MonoBehaviour
{
    [SerializeField]
    float EnterDistance = 3f;

    [SerializeField]
    GameObject EnterPrompt;

    [SerializeField]
    List<KeyCode> InteractKeys;

    bool IsOpen = false;

    bool subscribed = false;
    void Update()
    {
        if (!subscribed)
        {
            MuliplayerController.instance.OpenTavern += OpenTavern;
            subscribed = true;
        }

        if(MuliplayerController.instance.Keys >= 3 && CheckPlayerDistance())
        {
            EnterPrompt.SetActive(true);
            foreach(KeyCode key in InteractKeys)
            {
                if (Input.GetKey(key))
                {
                    EnterTavern();
                }
            }
        }
        else
        {
            EnterPrompt.SetActive(false);
        }
    }

    bool CheckPlayerDistance()
    {
        foreach(var player in MuliplayerController.instance.PlayerTransforms)
        {
            if(Vector3.Distance(player.transform.position, transform.position) <= EnterDistance)
            {
                return true;
            }
        }
        return false;
    }
    void OpenTavern()
    {
        IsOpen = true;
    }

    void EnterTavern()
    {
        //load next scene
        SceneManager.LoadScene("TavernScene");
    }
}
