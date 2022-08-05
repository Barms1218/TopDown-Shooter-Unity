using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected GameObject child;
    protected Transform lookAtTransform;
    protected bool facingRight = true;

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    protected virtual void Start()
    {


    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (lookAtTransform != null)
        {
            if (lookAtTransform.position.x < transform.position.x
                && facingRight && Time.deltaTime != 0)
            {
                Flip();
            }
            else if (lookAtTransform.position.x > transform.position.x
                && !facingRight && Time.deltaTime != 0)
            {
                Flip();
            }
        }
    }

    public abstract void MoveObject(Vector2 moveInput);

    protected virtual void Flip()
    {
        Vector3 newScale = child.transform.localScale;
        newScale.x *= -1f;

        facingRight = !facingRight;

        child.transform.localScale = newScale;
    }
}
