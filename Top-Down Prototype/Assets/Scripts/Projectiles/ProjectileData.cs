using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet")]
public class ProjectileData : ScriptableObject
{
    [SerializeField] int damage;
    [SerializeField] float timeToLive;
    [SerializeField] float speed;

    public int Damage
    {
        get => damage;
        set => damage = value;
    }
    public float TimeToLive => timeToLive;
    public float Speed => speed;
}
