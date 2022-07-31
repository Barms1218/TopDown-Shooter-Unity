using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponHandler : MonoBehaviour
{
    [SerializeField] GameObject gun;
    [SerializeField] float maximumRange;
    [SerializeField] float minimumRange;
    [SerializeField] Weapon weapon;
    private Transform playerTransform;
    float nextTriggerPull;
    Vector2 aimDirection;

    private void Start()
    {
        playerTransform = PlayerController.player.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        Aim();
    }

    private void Aim()
    {
        if (playerTransform != null)
        {
            aimDirection = playerTransform.position - gun.transform.position;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            weapon.Aim(angle, playerTransform.position);

            var _distance = Vector2.Distance(playerTransform.position, transform.position);
            if (_distance > minimumRange && _distance < maximumRange && CanFire)
            {
                OnFire();
                nextTriggerPull = Time.time + weapon.TimeBetweenShots;
            }
        }
    }

    private void OnFire() => weapon.Fire(aimDirection);


    private bool CanFire => Time.time >= nextTriggerPull;
}
