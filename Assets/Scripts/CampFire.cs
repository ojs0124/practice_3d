using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    public int damage;
    public float latency;

    private List<IDamagable> objects = new List<IDamagable>();

    private void Start()
    {
        InvokeRepeating("DealDamage", 0, latency);
    }

    void DealDamage()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].TakePhysicalDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out IDamagable damagable))
        {
            objects.Add(damagable);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out IDamagable damagable))
        {
            objects.Remove(damagable);
        }
    }
}
