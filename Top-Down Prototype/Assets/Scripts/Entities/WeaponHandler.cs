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
    protected abstract void Fire();

    protected virtual void SpecialAttack()
    {
        currentWeapon.SpecialAttack();
    }

    protected virtual bool CanFire => Time.time >= nextTriggerPull;

}
