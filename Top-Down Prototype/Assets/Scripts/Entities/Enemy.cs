using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class Enemy : MonoBehaviour, IFlippable
{
    protected GameObject player;
    AIPath path;
    protected bool facingRight = true;
    protected StateMachine stateMachine;
    
    protected int points;


    protected int Points
    {
        set
        {
            points = value;
        }
        get => points;
    }


    protected virtual void Start()
    {
        path = GetComponent<AIPath>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void Update()
    {
        //path.destination = player.transform.position; // Send object at player
 
        // Use boolean to logically decide if flipping is necessary
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


    /// <summary>
    /// 
    /// </summary>
    public virtual void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1f;

        facingRight = !facingRight;

        gameObject.transform.localScale = newScale;
    }

    /// <summary>
    /// Stop following player, begin fading out, and destroy 
    /// </summary>
    public void Die()
    {
        path.enabled = false;

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
