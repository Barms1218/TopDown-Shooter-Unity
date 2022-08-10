using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Integer")]
public class IntVariable : ScriptableObject
{
    [SerializeField] int intValue;

    public int Value
    {
        get => intValue;
        set => intValue = value;
    }

    private void OnDisable()
    {
        intValue = 0;
    }
}
