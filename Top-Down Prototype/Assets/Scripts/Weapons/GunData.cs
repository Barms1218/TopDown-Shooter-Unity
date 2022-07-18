using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    [SerializeField] float fireRate;
    [SerializeField] int maxAmmo;
    [SerializeField] float reloadSpeed;
    [SerializeField] float timeBetweenShots;
    [SerializeField] int magazineSize;
    [SerializeField] int currentAmmo;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField, Range(0, 1)] float recoil;

    public float FireRate => fireRate;
    public int MaxAmmo => maxAmmo;
    public int MagazineSize => magazineSize;
    public float ReloadSpeed => reloadSpeed;
    public int CurrentAmmo => currentAmmo;
    public GameObject ProjectilePrefab => projectilePrefab;
    public float Recoil => recoil;
    public float TimeBetweenShots => timeBetweenShots;
}
