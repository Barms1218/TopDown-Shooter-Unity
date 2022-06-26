using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponHandler : WeaponHandler
{
    private GameObject player;
    private bool facingRight;
    private Enemy settings;    

    private void Awake()
    {
        settings = GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentWeapon = gun.GetComponent<Weapon>();
    }

    // Update is called once per frame
    private void Update()
    {
        aimDirection = player.transform.position - gun.transform.position;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        gun.GetComponent<Weapon>().Aim(angle, player.transform);

        if (player != null)
        {
            var _distance = Vector2.Distance(player.transform.position, transform.position);
            if (_distance < settings.AttackRange)
            {
                Fire();
            }
        }

        if (currentWeapon.CurrentAmmo <= 0)
        {
            base.Reload();
        }
    }

    protected override void SpecialAttack()
    {
        return;
    }
}
