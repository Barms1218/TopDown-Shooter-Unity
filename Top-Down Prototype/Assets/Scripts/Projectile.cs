using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToTarget(Vector2 force)
    {
        float forceMagnitude = 7f;
        body.AddForce(force * forceMagnitude, ForceMode2D.Impulse);
    }
}
