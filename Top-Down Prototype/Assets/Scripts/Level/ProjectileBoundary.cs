using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBoundary : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(collision.gameObject);
        }
    }
}
