using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToTarget(Vector2 force)
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>(); 
        ;
        float forceMagnitude = 7f;
        body.AddForce(force * forceMagnitude, ForceMode2D.Impulse);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
