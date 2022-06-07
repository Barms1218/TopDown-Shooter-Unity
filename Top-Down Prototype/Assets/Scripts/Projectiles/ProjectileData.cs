using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile")]
public class ProjectileData : ScriptableObject
{
    [SerializeField]
    new string name;
    [SerializeField]
    float damage;
    [SerializeField]
    float timeToLive;
    [SerializeField]
    float amountOfForce;

    public float Damage => damage;
    public float TimeToLive => timeToLive;
    public string Name => name;
    public float AmountOfForce => amountOfForce;
}
