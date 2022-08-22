using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackObject : ScriptableObject
{
    [SerializeField] private int attackStrength;
    [SerializeField] private int damage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private AudioClipObject meleeClip;

    public int AttackStrength => attackStrength;
    public int Damage => damage;
    public float AttackCoolDown => attackCooldown;
    public AudioClipObject AttackSound => meleeClip;
}
