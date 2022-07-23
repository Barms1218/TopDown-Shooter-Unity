using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponHandler : MonoBehaviour
{
    [SerializeField] GameObject gun;
    [SerializeField] float attackRange;
    [SerializeField] Weapon weapon;
    private GameObject player;
    float nextTriggerPull;
    Vector2 aimDirection;


    // Update is called once per frame
    private void Update()
    {
        Aim();
        player = PlayerController.player.gameObject;
    }

    private void Aim()
    {
        if (player != null)
        {
            aimDirection = player.transform.position - gun.transform.position;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            weapon.Aim(angle, player.transform);

            var _distance = Vector2.Distance(player.transform.position, transform.position);
            if (_distance < attackRange && CanFire)
            {
                OnFire();
                nextTriggerPull = Time.time + weapon.TimeBetweenShots;
            }
        }
    }

    private void OnFire() => weapon.Fire(aimDirection);


    private bool CanFire => Time.time >= nextTriggerPull;
}
