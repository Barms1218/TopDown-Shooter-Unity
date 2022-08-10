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

    private void Awake()
    {
        enemyMove = GetComponent<Move>();
        attack = GetComponent<IAttack>();
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

    private IEnumerator PauseMovement()
    {
        enemyMove.enabled = false;
        yield return new WaitForSeconds(1f);
        enemyMove.enabled = true;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
}
