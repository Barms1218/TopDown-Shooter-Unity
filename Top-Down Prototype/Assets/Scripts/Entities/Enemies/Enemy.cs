using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    private int points;
    [SerializeField] private EnemySettings settings;

    private int Points
    {
        set
        {
            points = value;
        }
        get => points;
    }

    public float Speed => settings.Speed;
    public float AttackRange => settings.AttackRange;
    public float AttackStrength => settings.AttackStrength;
    public int Damage => settings.Damage;
    public float Cooldown => settings.CoolDown;
}
