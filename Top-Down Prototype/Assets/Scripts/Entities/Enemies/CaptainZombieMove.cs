using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptainZombieMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Animator _animator;
    [SerializeField] float maxDistance = 2f;
    [SerializeField] float moveAgainTime = 2f;
    float moveTimer = 0;
    Vector2 newPosition;


    // Update is called once per frame
    private void Update()
    {

        if (Time.time >= moveTimer)
        {
            ChangePosition();
            moveTimer = Time.time + moveAgainTime;
        }
    }

    private void FixedUpdate()
    {
        var movePosition = Vector2.MoveTowards(
            transform.position, newPosition, 1f * Time.deltaTime);
        rb2d.MovePosition(movePosition);
    }

    void ChangePosition()
    {
        newPosition = new Vector3(Random.Range(-maxDistance, maxDistance),
            Random.Range(-maxDistance, maxDistance), 0);
    }
}
