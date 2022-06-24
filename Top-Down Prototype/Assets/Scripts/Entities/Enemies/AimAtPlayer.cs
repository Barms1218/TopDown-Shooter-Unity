using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtPlayer : MonoBehaviour
{
    [SerializeField] GameObject gun;
    // Update is called once per frame
    void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var aimDirection = player.transform.position - transform.position;
        float xDirection = Random.Range(aimDirection.x - 2, aimDirection.x + 2);
        float yDirection = Random.Range(aimDirection.y - 2, aimDirection.y + 2);
        float angle = Mathf.Atan2(xDirection, yDirection) * Mathf.Rad2Deg;
        gun.GetComponent<Weapon>().Aim(angle);
    }
}
