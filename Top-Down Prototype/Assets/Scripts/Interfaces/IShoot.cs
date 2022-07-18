using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShoot
{
    float ReloadSpeed { get; }
    float TimeBetweenShots { get; }
    int MaxAmmo { get; }
    int CurrentAmmo { get; }
    int MagazineSize { get; }

    void Shoot(Vector2 direction);

    void Aim(float angle, Transform target);

    void Reload();

    void SpecialAttack();
}
