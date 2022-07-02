using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMove : MonoBehaviour
{
    private GameObject player;
    private bool facingRight = true;
    private bool canMove = true;
    private float moveSpeed;
    [SerializeField] private float chaseDistance;
    public static UnityAction OnEngage;
    
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        MeleeAttack.OnMelee += ChangeMoveState;
        moveSpeed = Random.Range(2.7f, 3.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) > chaseDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, 
            player.transform.position, Time.deltaTime * moveSpeed);
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
