using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UIEnvSet : MonoBehaviour
{
    public GameObject EnvSetWindow;

    private PlayerCondition condition;
    private PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        condition = CharacterManager.Instance.Player.condition;
        controller = CharacterManager.Instance.Player.controller;

        EnvSetWindow.SetActive(false);
        controller.envSet += Toggle;
    }

    public void Toggle()
    {
        if (IsOpen())
        {
            EnvSetWindow.SetActive(false);
        }
        else
        {
            EnvSetWindow.SetActive(true);
        }
    }

    public bool IsOpen()
    {
        return EnvSetWindow.activeInHierarchy;
    }
}
