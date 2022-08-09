using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject flipObject;
    [SerializeField] private string transformName;
    private Rigidbody2D rb2d;
    private Animator animator;
    Transform lookAtTransform;
    private bool facingRight = true;

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

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

    public void MoveObject(Vector2 moveInput)
    {
        rb2d.MovePosition(rb2d.position + speed * Time.deltaTime * moveInput);
        animator.SetBool("Running", true);
        if (moveInput == Vector2.zero)
        {
            animator.SetBool("Running", false);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 newScale = flipObject.transform.localScale;
        newScale.x *= -1f;
        flipObject.transform.localScale = newScale;
    }
}
