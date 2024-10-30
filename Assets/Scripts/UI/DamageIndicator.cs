using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    public Image image;
    public float flashSpeed;

    private Coroutine coroutine;

    private void Start()
    {
        CharacterManager.Instance.Player.condition.OnTakeDamage += Flash;
    }

    public void Flash()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        image.enabled = true;
        image.color = new Color(1f, 0f, 0f);
        coroutine = StartCoroutine(FadeAway());
    }

    private IEnumerator FadeAway()
    {
        float duration = 0.25f;
        float fadeRate = duration;

        while (fadeRate > 0.0f)
        {
            fadeRate -= (duration / flashSpeed) * Time.deltaTime;
            image.color = new Color(1f, 0f, 0f, fadeRate);
            yield return null;
        }

        image.enabled = false;
    }
}
