using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    [SerializeField] GameObject gun;
    [SerializeField] float fireRate;
    [SerializeField] int startAmmo;
    [SerializeField] int maxAmmo;
    [SerializeField] float reloadSpeed;
    [SerializeField] int magazineSize;
    [SerializeField] int currentAmmo;
    [SerializeField, Range(0, 1)] float recoil;


    public GameObject Gun => gun;
    public float FireRate => fireRate;
    public int StartAmmo => startAmmo;
    public int MaxAmmo
    {
        get => maxAmmo;
        set => maxAmmo = value;
    }
    public int CurrentAmmo
    {
        get => currentAmmo;
        set => currentAmmo = value;
    }
    public int MagazineSize => magazineSize;
    public float ReloadSpeed => reloadSpeed;

    public float Recoil => recoil;

    private void OnEnable()
    {
        currentAmmo = magazineSize;
        maxAmmo = startAmmo;
    }
}
