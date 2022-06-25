using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile")]
public class ProjectileData : ScriptableObject
{
    [SerializeField] int damage;
    [SerializeField] float timeToLive;
    [SerializeField] float amountOfForce;
    [SerializeField,Range(0, 1)] float projectileForce;

    public int Damage => damage;
    public float TimeToLive => timeToLive;
    public float AmountOfForce => amountOfForce;
    public float ProjectileForce => projectileForce;
}
