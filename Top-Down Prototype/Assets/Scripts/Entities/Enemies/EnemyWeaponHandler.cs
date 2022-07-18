using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponHandler : WeaponHandler
{
    [SerializeField] float attackRange;
    private GameObject player;   

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentWeapon = gun.GetComponent<Weapon>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (player != null)
        {
            aimDirection = player.transform.position - gun.transform.position;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            currentWeapon.Aim(angle, player.transform);
            var _distance = Vector2.Distance(player.transform.position, transform.position);
            if (_distance < attackRange && CanFire)
            {
                OnFire();
                nextTriggerPull = Time.time + currentWeapon.TimeBetweenShots;
            }
        }
    }

    public override void OnFire()
    {

        currentWeapon.Fire(aimDirection);
  
    }
    public override void SpecialAttack()
    {
        return;
    }
}
