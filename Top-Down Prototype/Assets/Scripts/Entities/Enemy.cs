using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class Enemy : Entity
{
    GameObject player;
    AIPath path;

    int points;


    int Points
    {
        set
        {
            points = value;
        }
        get => points;
    }


    protected override void Start()
    {
        base.Start();
        path = GetComponent<AIPath>();
        player = GameObject.FindGameObjectWithTag("Player");


    }

    protected override void Update()
    {
        path.destination = player.transform.position; // Send object at player

        GetInput();
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

    protected override void GetInput()
    {
        base.GetInput();
        if (path.velocity != Vector3.zero)
        {
            state = State.STATE_MOVE;
        }
    }

    /// <summary>
    /// Stop following player, begin fading out, and destroy 
    /// </summary>
    protected override void Die()
    {
        path.enabled = false;

        SpriteRenderer spriteRenderer =  gameObject.GetComponent<SpriteRenderer>();

        StartCoroutine(FadeOut(spriteRenderer, 1.5f));
        state = State.STATE_DYING;
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
