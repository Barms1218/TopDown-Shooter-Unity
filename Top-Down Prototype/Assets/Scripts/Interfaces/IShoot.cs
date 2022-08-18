using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShoot
{
    public GameObject Gun { get; }
    public bool CanRapidFire { get; }
    public int CurrentAmmo { get; }
    public int MagazineSize { get; }
    public int MaxAmmo { get; set; }
    public float FireRate { get; }
    public bool Reloading { get; }
    public void Fire(Vector2 direction);
    public IEnumerator Reload();
}
