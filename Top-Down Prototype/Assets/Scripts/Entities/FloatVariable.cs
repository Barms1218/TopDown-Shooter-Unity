using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "float")]
public class FloatVariable : ScriptableObject
{
    [SerializeField] int maxValue;
    [NonSerialized] public float runtimeValue;

    public int MaxValue
    {
        get => maxValue;
    }

    public void OnEnable()
    {
        runtimeValue = maxValue;
    }
}