using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Animator _animator;
    [SerializeField] float maxDistance = 2f;
    [SerializeField] float moveAgainTime = 2f;
    [SerializeField] float moveTimer = 0;
    Vector2 newPosition;


    // Update is called once per frame
    private void Update()
    {
        transform.position = Vector2.MoveTowards(
           transform.position, newPosition, 1f * Time.deltaTime);
        if (Time.time >= moveTimer)
        {
            ChangePosition();
            moveTimer = Time.time + moveAgainTime;
        }
    }

    void ChangePosition()
    {
        newPosition = new Vector3(Random.Range(-maxDistance, maxDistance),
            Random.Range(-maxDistance, maxDistance), 0);
    }
}
