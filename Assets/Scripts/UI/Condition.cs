using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public Image bar;

    public float startValue;
    public float maxValue;
    public float currentValue;
    public float passiveValue;

    void Start()
    {
        currentValue = startValue;
    }

    void Update()
    {
        bar.fillAmount = GetPercent();
    }

    public void Add(float amount)
    {
        currentValue = Mathf.Min(currentValue + amount, maxValue);
    }

    public void Sub(float amount)
    {
        currentValue = Mathf.Max(currentValue - amount, 0.0f);
    }

    public float GetPercent()
    {
        return currentValue / maxValue;
    }
}
