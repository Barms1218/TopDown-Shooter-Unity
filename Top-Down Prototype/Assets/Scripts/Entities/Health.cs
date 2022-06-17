using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    protected int maxHealth = 100;

    int _health;
    StateMachine stateMachine;

    public UnityAction OnDied;
    // Start is called before the first frame update
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _health = maxHealth;
        stateMachine = GetComponent<StateMachine>();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="damage"></param>
    public virtual void TakeDamage(int damage)
    {
        this._health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Stop following player, begin fading out, and destroy 
    /// </summary>
    void Die()
    {
        //OnDied?.Invoke();
        SpriteRenderer spriteRenderer =  gameObject.GetComponent<SpriteRenderer>();

        StartCoroutine(FadeOut(spriteRenderer, 1.5f));
        Destroy(gameObject, 1.5f);
    }

    /// <summary>
    /// Reduce object's alpha to 0 over duration passed into
    /// paramater by comparing the duration with an incrementing value
    /// </summary>
    /// <param name="spriteRenderer"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
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
