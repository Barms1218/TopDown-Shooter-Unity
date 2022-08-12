using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    private float speed = 2.5f;
    private float sprintSpeed = 5f;
    [SerializeField]
    private float jumpForce = 10f;
    bool isGrounded = true;
    bool canDash = true;
    Vector3 moveInput;

    public bool IsGrounded => isGrounded;

    public bool CanDash => canDash;

    public void MoveObject(Vector3 input)
    {
        moveInput = input;
        _rigidbody.MovePosition(transform.position + speed * Time.deltaTime * input);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(CoyoteTime());
        }
    }

    public void Jump()
    {
        Debug.Log("Jumping");
        _rigidbody.AddForce(new Vector3(0f, jumpForce, moveInput.z), ForceMode.Impulse);
        isGrounded = false;
    }

    private IEnumerator CoyoteTime()
    {
        yield return new WaitForSeconds(0.2f);
        isGrounded = false;
    }

    public IEnumerator Dodge()
    {
        canDash = false;

        speed *= 2;
        yield return new WaitForSeconds(0.4f);
        speed /= 2;

        yield return new WaitForSeconds(2f);
        canDash = true;
    }

    public IEnumerator Sprint()
    {
        while (true)
        {
            speed = 5f;
            yield return null;
        }
    }
}

