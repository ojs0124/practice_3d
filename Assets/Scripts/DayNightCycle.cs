using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float time;
    public float dayLength;
    public float startTime = 0.5f;
    private float timeRate;
    public Vector3 noon;

    [Header("Sun")]
    public Light sun;
    public Gradient sunGradient;
    public AnimationCurve sunIntensity;

    [Header("Moon")]
    public Light moon;
    public Gradient moonGradient;
    public AnimationCurve moonIntensity;

    [Header("AnimationCurve")]
    public AnimationCurve lightingIntensityMultiplier;
    public AnimationCurve reflectionIntensityMultiplier;

    private void Start()
    {
        time = startTime;
        timeRate = 1.0f / dayLength;
    }

    private void Update()
    {
        time = (time + timeRate * Time.deltaTime) % 1.0f;

        UpdateLighting(sun, sunGradient, sunIntensity);
        UpdateLighting(moon, moonGradient, moonIntensity);

        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);

    }

    void UpdateLighting(Light lightSource, Gradient colorGradiant, AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(time);
        lightSource.transform.eulerAngles = (time - (lightSource == sun ? 0.25f : 0.75f)) * noon * 4.0f;
        lightSource.color = colorGradiant.Evaluate(time);
        lightSource.intensity = intensity;

        GameObject gameObject = lightSource.gameObject;
        if (lightSource.intensity == 0 && gameObject.activeInHierarchy)
            gameObject.SetActive(false);
        else if (lightSource.intensity > 0 && !gameObject.activeInHierarchy)
            gameObject.SetActive(true);
    }
}
