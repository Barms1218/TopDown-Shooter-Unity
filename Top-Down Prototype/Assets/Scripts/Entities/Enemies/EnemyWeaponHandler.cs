using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponHandler : MonoBehaviour
{
    [SerializeField] AIController _controller;
    [SerializeField] GameObject gun;
    [SerializeField] float maximumRange;
    [SerializeField] float minimumRange;
    [SerializeField] Weapon weapon;
    private Transform playerTransform;
    float nextTriggerPull;
    Vector2 aimDirection;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
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
        }
    }

    private void Attack()
    {
        var _distance = Vector2.Distance(playerTransform.position, transform.position);
        if (CanFire)
        {
            weapon.Fire(aimDirection);
            nextTriggerPull = Time.time + weapon.TimeBetweenShots;
        }

    }

    private bool CanFire => Time.time >= nextTriggerPull;

    private void OnEnable()
    {
        _controller.attackDelegate += Attack;
    }

    private void OnDisable()
    {
        _controller.attackDelegate -= Attack;
    }
}
