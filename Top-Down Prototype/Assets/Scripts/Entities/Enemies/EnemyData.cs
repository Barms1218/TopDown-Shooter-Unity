using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/New Enemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField] string targetTag;
    [SerializeField] float startingHealth;
    [SerializeField] float minimumAttackDistance;
    [SerializeField] float maximumAttackDistance;
    [SerializeField] float chaseDistance;
    [SerializeField] float attackCooldown;
    [SerializeField] AttackObject attackType;

    public string TargetTag => targetTag;
    public float StartingHealth => startingHealth;
    public float MinAttackDistance => minimumAttackDistance;
    public float MaxAttackDistance => maximumAttackDistance;
    public float ChaseDistance => chaseDistance;
    public float AttackCooldown => attackCooldown;
    public AttackObject AttackType => attackType;
}
