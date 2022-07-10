using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float chaseDistance;
    [SerializeField] private float aggroRadius;
    private Transform startPos;
    private GameObject player;
    private bool facingRight = true;
    private bool isChasingPlayer = false;
    private float moveSpeed;

    public static UnityAction OnEngage;

    private Animator _animator;
    
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //MeleeAttack.OnMelee += ChangeMoveState;
        moveSpeed = Random.Range(2.7f, 3.2f);
        GetComponent<Health>().OnHit += HitByPlayer;
        GetComponent<KeepTrackOfPlayer>().OnSightedPlayer += HitByPlayer;
        _animator = GetComponent<Animator>();
        startPos = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.transform.position,
            transform.position) >= chaseDistance && isChasingPlayer)
        {
            //GetComponent<Rigidbody2D>().MovePosition(player.transform.position * Time.deltaTime * moveSpeed);
            transform.position = Vector2.MoveTowards(transform.position,
            player.transform.position, Time.deltaTime * moveSpeed);
            _animator.Play("Running");
        }
        else if (!isChasingPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                startPos.position, Time.deltaTime * moveSpeed);
            _animator.SetBool("Running", false);
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
    public void HitByPlayer(bool isAngry)
    {
        isChasingPlayer = isAngry;
        
    }
}
