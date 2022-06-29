using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponHandler : WeaponHandler
{
    private GameObject player;
    private bool facingRight;
    private EnemySettings rangedAttackData;    

    private void Awake()
    {
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
            if (_distance < rangedAttackData.AttackRange)
            {
                Fire();
            }
        }
    }

    protected override void Fire()
    {
        if (CanFire)
        {
            currentWeapon.Fire(aimDirection);
            nextTriggerPull = Time.time + currentWeapon.TimeBetweenShots;            
        }   
    }
    protected override void SpecialAttack()
    {
        return;
    }
}
