using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health Object", menuName = "Health/New Health Object")]
public class HealthObject : ScriptableObject
{
    [SerializeField] int maxValue;
    [NonSerialized] float runtimeValue;

    public int MaxValue
    {
        get => maxValue;
    }

    public void OnEnable()
    {
        runtimeValue = maxValue;
    }
}
