using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private int attackStrength;
    [SerializeField] private float attackRange;
    [SerializeField] private int damage;
    [SerializeField] private float attackCooldown;
    private float nextAttack;
    private float recoverTime;
    private GameObject player;
    public static UnityAction<bool, MeleeAttack> OnMelee;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        var _distance = Vector2.Distance(player.transform.position, transform.position);
        if( _distance <= attackRange)
            Attack();
    } 

    void Attack()
    {
        var _health = player.GetComponent<IHaveHealth>();
        if (CanAttack() && _health != null)
        {
            AudioManager.Play(AudioClipName.MeleeAttack);
            _health.TakeDamage(damage, this.gameObject, attackStrength);
            StartCoroutine(RecoverFromAttack());
            nextAttack = Time.time + attackCooldown;
        }
    }


    private IEnumerator RecoverFromAttack()
    {
        recoverTime = 0f;
        
        while (recoverTime < 1)
        {
            recoverTime += Time.deltaTime;
            OnMelee?.Invoke(false, this);
            GetComponent<Animator>().SetBool("Running", false);
            yield return null;
        }
        OnMelee?.Invoke(true, this);
    }

    private bool CanAttack() => Time.time >= nextAttack;
}
