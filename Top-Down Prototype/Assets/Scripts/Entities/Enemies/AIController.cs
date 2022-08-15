using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Move))]
public class AIController : MonoBehaviour
{
    [SerializeField] EnemyData data;
    IAttack attack;
    private Transform targetTransform;
    private Move enemyMove;
    private float distanceToTarget;
    private float nextAttack;

    private Collider2D _collider;

    private void Awake()
    {
        enemyMove = GetComponent<Move>();
        attack = GetComponent<IAttack>();
        _collider = GetComponent<Collider2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        targetTransform = GameObject.FindGameObjectWithTag(data.TargetTag).transform;
    }

    private void Update()
    {
        if (targetTransform != null)
        {
            distanceToTarget = Vector2.Distance(targetTransform.position, transform.position);
            if (distanceToTarget > data.MinAttackDistance &&
                distanceToTarget < data.MaxAttackDistance)
            {
                if (Time.time >= nextAttack && Time.timeScale > 0)
                {
                    attack.Attack();
                    //StartCoroutine(PauseMovement());
                    nextAttack = Time.time + data.AttackCooldown;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        var moveInput = targetTransform.position - transform.position;
        var speed = data.Speed;
        if (moveInput != null && distanceToTarget > data.ChaseDistance)
        {
            enemyMove.MoveObject(moveInput.normalized, speed);
        }
    }

    private void OnEnable()
    {
        _collider.enabled = true;
    }

    private void OnDisable()
    {
        _collider.enabled = false;
    }
}
