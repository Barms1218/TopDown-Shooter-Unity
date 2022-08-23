using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private GameObject flipObject;
    [SerializeField] private string transformName;
    private Rigidbody2D rb2d;
    private Animator animator;
    Transform lookAtTransform;
    private bool facingRight = true;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        lookAtTransform = GameObject.FindGameObjectWithTag(transformName).transform;
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

    public void MoveObject(Vector2 moveInput, float speed)
    {
        rb2d.MovePosition(rb2d.position + speed * Time.deltaTime * moveInput);
        //animator.SetFloat("Move Magnitude", moveInput.magnitude);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 newScale = flipObject.transform.localScale;
        newScale.x *= -1f;
        flipObject.transform.localScale = newScale;
    }
}
