using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] InputController input = null;
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;

    Rigidbody2D body;
    Vector2 direction;
    Vector2 velocity;
    Vector2 desiredVelocity;


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = input.RetrieveHorizontalInput();
        direction.y = input.RetrieveVerticalInput();
        desiredVelocity = new Vector2(direction.x, direction.y)
            * Mathf.Max(maxSpeed, 0f);
    }
    private void FixedUpdate()
    {
        velocity = body.velocity;

        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, 5f);
        velocity.y = Mathf.MoveTowards(velocity.y, desiredVelocity.y, 5f);

        body.velocity = velocity;
    }
}
