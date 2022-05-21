using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] InputController input = null;
    [SerializeField] GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (input.RetrieveMouseButtonZero())
        {
            Fire();
        }
    }

    private void Fire()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - transform.position;

        var projectile = Instantiate(projectilePrefab,
            transform.position, Quaternion.identity);

        Projectile projectileScript = projectile.GetComponent<Projectile>();

        projectileScript.MoveToTarget(direction.normalized);
    }
}
