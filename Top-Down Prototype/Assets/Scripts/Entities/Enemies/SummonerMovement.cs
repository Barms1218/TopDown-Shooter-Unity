using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerMovement : EnemyMovement
{
    Vector2 newPosition;
    float maxDistance = 2f;
    float moveAgainTime = 2f;
    float moveTimer = 0;



    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
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
