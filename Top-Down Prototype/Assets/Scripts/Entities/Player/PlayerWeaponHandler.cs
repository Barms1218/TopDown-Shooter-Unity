using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    private List<GameObject> weaponList = new List<GameObject>();
    private float timeToTriggerPull;
    private Weapon currentWeapon;
    private Vector2 aimDirection;


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
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        weaponList.Add(gun);
        currentWeapon = gun.GetComponent<Weapon>();        
        PlayerInput.OnReload += Reload;
        PlayerInput.OnFire += Fire;
        PlayerInput.OnSpecialAttack += SpecialAttack;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = mousePos - transform.position;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;        
        currentWeapon.Aim(angle);
        currentWeapon.AimAngle = angle;
    }

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

    private bool CanFire() => Time.time >= timeToTriggerPull;
}
