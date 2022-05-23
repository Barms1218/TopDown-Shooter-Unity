using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : Weapon
{

    protected override void Start()
    {
        base.Start();
    }

    public override void Shoot()
    {
        base.Shoot();
    }


    public override void Reload()
    {
        base.Reload();
    }

    public override void SpecialAttack()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }
}
