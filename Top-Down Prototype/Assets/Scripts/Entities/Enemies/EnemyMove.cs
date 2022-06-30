using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMove : MonoBehaviour
{
    private Enemy enemy;
    private GameObject player;
    private bool facingRight = true;
    private bool canMove = true;

    public static UnityAction OnEngage;
    
    // Start is called before the first frame update
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player");
        MeleeAttack.OnMelee += ChangeMoveState;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) > enemy.AttackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, 
            player.transform.position, Time.deltaTime * enemy.Speed);
            GetComponent<Animator>().SetBool("Running", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Running", false);
        }

        if (player.transform.position.x > transform.position.x
            && !facingRight)
        {
            Flip();
        }
        else if (player.transform.position.x < transform.position.x
            && facingRight)
        {
            Flip();
        }           
    }

    private void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1f;

        facingRight = !facingRight;

        gameObject.transform.localScale = newScale;
    }

    private void ChangeMoveState(bool moveBool, MeleeAttack attackingEnemy)
    {
        var attackingEnemyMovement = attackingEnemy.GetComponent<EnemyMove>();
        attackingEnemyMovement.enabled = moveBool;
    }    
}
