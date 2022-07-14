using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerZombie : Enemy
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnRate = 5f;
    float maxDistance = 2f;
    float moveAgainTime = 2f;
    float moveTimer = 0;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        InvokeRepeating("Summon", 0, spawnRate);
        _animator.SetBool("Running", true);
    }

    // Update is called once per frame
    protected override void Update()
    {
        transform.position = Vector2.MoveTowards(
           transform.position, newPosition, 1f * Time.deltaTime);
        if (Time.time >= moveTimer)
        {
            ChangePosition();
            moveTimer = Time.time + moveAgainTime;
        }

        if (_player.transform.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (_player.transform.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
    }

    private void Summon()
    {
        var minion = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
    void ChangePosition()
    {
        newPosition = new Vector3(Random.Range(-maxDistance, maxDistance),
            Random.Range(-maxDistance, maxDistance), 0);
    }
}
