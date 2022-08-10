using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/New Enemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField] string targetTag;
    [SerializeField] float minimumAttackDistance;
    [SerializeField] float maximumAttackDistance;
    [SerializeField] float chaseDistance;
    [SerializeField] float attackCooldown;
    [SerializeField] float speed;

    public string TargetTag => targetTag;
    public float MinAttackDistance => minimumAttackDistance;
    public float MaxAttackDistance => maximumAttackDistance;
    public float ChaseDistance => chaseDistance;
    public float AttackCooldown => attackCooldown;
    public float Speed
    {
        get => speed;
        set => speed = value;
    }
}
