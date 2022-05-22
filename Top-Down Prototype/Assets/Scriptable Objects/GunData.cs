using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    public new string name;
    [Header("Shoot Data")]
    public float damage;
    public float range;
    public float fireRate;
    [Header("Reload Data")]
    public int currentAmmo;
    public int maxAmmo;
    public float reloadSpeed;
    public bool reloading;

}
