using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private int attackStrength;
    [SerializeField] private int damage;
    [SerializeField] private float attackCooldown;
    private float nextAttack;
    private float recoverTime;
    private Enemy settings;
    private GameObject player;
    public static UnityAction<bool, MeleeAttack> OnMelee;

    // Start is called before the first frame update
    private void Awake()
    {
        settings = GetComponent<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player");
    } 

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    private void OnCollisionEnter2D(Collision2D other)
    {
        var _health = other.gameObject.GetComponent<IHaveHealth>();
        if (other.gameObject.tag == "Player")
        {
            if (CanAttack() && _health != null)
            {
                _health.TakeDamage(damage, this.gameObject, attackStrength);
                nextAttack = Time.time + attackCooldown;
                StartCoroutine(RecoverFromAttack());
            }
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
