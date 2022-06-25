using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemies/New Enemy")]
public class EnemySettings : ScriptableObject
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private int damage = 5;
    [SerializeField] private float coolDown = 1f;
    [SerializeField] private float attackStrength = 60f;

    public float Speed => speed;
    public float AttackRange => attackRange;
    public int Damage => damage;
    public float CoolDown => coolDown;
    public float AttackStrength => attackStrength;
}
