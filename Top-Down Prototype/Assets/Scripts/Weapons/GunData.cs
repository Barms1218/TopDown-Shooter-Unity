using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    [SerializeField] float fireRate;
    [SerializeField] int maxAmmo;
    [SerializeField] int ammoPerShot;
    [SerializeField] float reloadSpeed;

    public float FireRate => fireRate;
    public int MaxAmmo => maxAmmo;
    public float ReloadSpeed => reloadSpeed;
    public int AmmoPerShot => ammoPerShot;

}
