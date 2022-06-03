using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    GameObject player;
    protected NavMeshAgent navMeshAgent;
    NavMeshPath path;
    protected override void Start()
    {
        base.Start();
        path = new NavMeshPath();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        //navMeshAgent.CalculatePath(FindObjectOfType<Player>().transform.position, path);
        if (player != null)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
    }

    protected override void Update()
    {
        
    }

    protected override void GetInput()
    {
        throw new System.NotImplementedException();
    }


    protected override void Die()
    {
        throw new System.NotImplementedException();
    }

}
