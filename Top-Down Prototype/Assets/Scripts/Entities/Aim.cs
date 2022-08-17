using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private IShoot gun;
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private string targetTag;
    private Transform targetTransform;
    private bool weaponFlipped = true;


    public IShoot Gun { get => gun; set => gun = value; }

    // Start is called before the first frame update
    void Start()
    {
        gun = GetComponentInChildren<IShoot>();
        targetTransform = GameObject.FindGameObjectWithTag(targetTag).transform;
        gun.Gun.transform.SetParent(weaponHolder, false);
    }

    private void Update()
    {
        var _direction = targetTransform.position - gun.Gun.transform.position;
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

        if (targetTransform.position.x > transform.position.x && !weaponFlipped)
        {
            FlipWeapon();
        }
        else if (targetTransform.position.x < transform.position.x && weaponFlipped)
        {
            FlipWeapon();
        }

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward * Time.deltaTime);
        gun.Gun.transform.rotation = rotation;
    }

    private void FlipWeapon()
    {
        weaponFlipped = !weaponFlipped;
        Vector3 newScale = weaponHolder.localScale;
        newScale.y *= -1;
        weaponHolder.localScale = newScale;
    }
}
