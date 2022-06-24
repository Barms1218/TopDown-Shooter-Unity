using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;

    int _health;

    public static UnityAction OnDied;

    float IDamageable.Health 
    { 
        get => _health; 
        set => _health = (int)value; 
    }

    public int MaxHealth => maxHealth;

    void Awake()
    {
        _health = maxHealth;
    }

    public void TakeDamage(int damage, GameObject damageSource)
    {
        _health -= damage;
        var pushDirection = gameObject.transform.position - damageSource.transform.position;
        Debug.Log(pushDirection * 20);
        GetComponent<Rigidbody2D>().AddForce(pushDirection.normalized * 100, ForceMode2D.Impulse);
        GetComponent<Animator>().SetTrigger("Hurt");
        if (_health <= 0)
        {
            Die();
        }
    } 
    public void RestoreHealth(int amount)
    {

    }

    public void Die()
    {
        SpriteRenderer spriteRenderer =  gameObject.GetComponent<SpriteRenderer>();

        StartCoroutine(FadeOut(spriteRenderer, 1.5f));

        Destroy(gameObject, 1.0f);
    }

    IEnumerator FadeOut(SpriteRenderer spriteRenderer, float duration)
    {
        float count = 0;

        Color color = spriteRenderer.material.color;

        while (count < duration)
        {
            count += Time.deltaTime;

            float alpha = Mathf.Lerp(1, 0, count / duration);

            spriteRenderer.color = new Color(color.r, color.g, color.b, alpha);

            yield return null;
        }
    }

}
