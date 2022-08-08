using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Move))]
public class AIController : MonoBehaviour
{
    [SerializeField] private float attackDistance;
    [SerializeField] private float minAttackDistance;
    [SerializeField] private float chaseDistance;
    private GameObject player;
    private Move enemyMove;
    private IAttack attack;
    private float distanceToTarget;
    public UnityAction attackDelegate;
    private float nextAttack;
    [SerializeField] private float attackCoolDown;

    private void Awake()
    {
        enemyMove = GetComponent<Move>();
        attack = GetComponent<IAttack>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (player != null)
        {
            distanceToTarget = Vector2.Distance(player.transform.position, transform.position);
            if (distanceToTarget > minAttackDistance && distanceToTarget < attackDistance)
            {
                 if (Time.time >= nextAttack && Time.timeScale > 0)
                {
                    attack.Attack();
                    nextAttack = Time.time + attackCoolDown;
                    attack.Attack();
                }

            }
        }
    }

    private void FixedUpdate()
    {
        var moveInput = player.transform.position - transform.position;
        if (moveInput != null && distanceToTarget > chaseDistance)
        {
            enemyMove.MoveObject(moveInput.normalized);
        }
    }
}
