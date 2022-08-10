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
    [SerializeField] float reloadSpeed;
    [SerializeField] int magazineSize;
    [SerializeField, Range(0, 1)] float recoil;


    public GameObject Gun => gun;
    public float FireRate => fireRate;
    public int StartAmmo => startAmmo;
    public int MagazineSize => magazineSize;
    public float ReloadSpeed => reloadSpeed;

    public float Recoil => recoil;
}
