using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(InputController))]
public class PlayerWeaponHandler : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject gun;
    private List<GameObject> weaponList = new List<GameObject>();
    private float timeToTriggerPull;
    private Weapon currentWeapon;
    private Vector2 aimDirection;

    #endregion

    #region Properties

    public GameObject Gun
    {
        set { gun = value; }
        get => gun;
    }
    public List<GameObject> WeaponList => weaponList;
    public Weapon CurrentWeapon
    {
        set { currentWeapon = value; }
        get => currentWeapon;
    }

    #endregion

    private void Awake()
    {
        weaponList.Add(gun);
        currentWeapon = gun.GetComponent<Weapon>();
        EntityInput.OnReload += Reload;
        EntityInput.OnSpecialAttack += SpecialAttack;
        EntityInput.OnFire += Fire;
    }

    private void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = mousePos - transform.position;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;        
        currentWeapon.Aim(angle);
        currentWeapon.AimAngle = angle;
    }

    #region Event Methods

    private void Reload()
    {
        currentWeapon.Reload();
    }

    private void Fire()
    {

        if (currentWeapon.CurrentAmmo > 0 && CanFire())
        {
            currentWeapon.Fire(aimDirection, currentWeapon);
            timeToTriggerPull = Time.time + currentWeapon.TimeBetweenShots;            
        }
    }

    private void SpecialAttack()
    {
        currentWeapon.SpecialAttack();
    }

    #endregion

    private bool CanFire() => Time.time >= timeToTriggerPull;
}
