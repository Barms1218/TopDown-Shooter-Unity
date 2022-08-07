using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] AIController _controller;
    [SerializeField] float maximumRange;
    [SerializeField] float minimumRange;
    [SerializeField] Weapon weapon;
    private Transform targetTransform;
    float nextTriggerPull;
    Vector2 _direction;
    private bool weaponFlipped = false;


    private bool CanFire => Time.time >= nextTriggerPull;

    private void Awake()
    {
        weapon.gameObject.transform.SetParent(transform, false);
    }
    private void Start()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    private void Update()
    {
        _direction = targetTransform.position - weapon.gameObject.transform.position;
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

        if (targetTransform.position.x > transform.position.x && weaponFlipped)
        {
            FlipWeapon();
        }
        else if (targetTransform.position.x < transform.position.x && !weaponFlipped)
        {
            FlipWeapon();
        }

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward * Time.deltaTime);
        weapon.transform.rotation = rotation;
    }

    private void FireWeapon()
    {
        if (CanFire)
        {
            weapon.Fire(_direction);
            nextTriggerPull = Time.time + weapon.TimeBetweenShots;
        }

    }

    private void FlipWeapon()
    {
        weaponFlipped = !weaponFlipped;
        Vector3 newScale = weapon.transform.localScale;
        newScale.y *= -1;

        weapon.transform.localScale = newScale;
    }

    private void OnEnable()
    {
        _controller.attackDelegate += FireWeapon;
    }

    private void OnDisable()
    {
        _controller.attackDelegate -= FireWeapon;
    }
}
