using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerCondition condition;
    public PlayerController controller;
    public Equipment equip;

    public ItemData itemData;
    public Action addItem;

    public Transform dropPosition;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;

        condition = GetComponent<PlayerCondition>();
        controller = GetComponent<PlayerController>();
        equip = GetComponent<Equipment>();
    }
}
