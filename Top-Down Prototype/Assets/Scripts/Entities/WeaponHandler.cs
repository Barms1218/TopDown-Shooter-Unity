using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponHandler : MonoBehaviour
{
    [SerializeField] protected GameObject gun;
    protected float nextTriggerPull;
    protected Weapon currentWeapon;
    protected Vector2 aimDirection;
    protected virtual void Reload()
    {
        currentWeapon.Reload();
    }
    protected virtual void Fire()
    {
        if (currentWeapon.CurrentAmmo > 0 && CanFire)
        {
            currentWeapon.Fire(aimDirection, currentWeapon);
            nextTriggerPull = Time.time + currentWeapon.TimeBetweenShots;            
        }        
    }
    protected abstract void SpecialAttack();
    protected virtual bool CanFire => Time.time >= nextTriggerPull;

}
