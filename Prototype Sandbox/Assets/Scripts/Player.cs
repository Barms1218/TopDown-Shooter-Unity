using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    #region Fields

    Rigidbody2D rb2d;

    // laser 
    LineRenderer lineRenderer;
    [SerializeField] Transform laserPoint;
    [SerializeField] float defaultRayDistance = 100f;
    Transform targetTransform;
    SwapEvent swapEvent = new SwapEvent();
    bool facingRight = true;

    // movement
    [SerializeField] float m_Speed = 50f;
    [SerializeField] float v_Force = 15f;
    [SerializeField] float m_Dampening = 10f;
    Vector2 moveVector;

    #endregion


    #region Unity Methods


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Jump");

        if (horizontalInput != 0)
        {
            moveVector.x = m_Speed * horizontalInput;
        }
        AimLaser();
    }

    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(moveVector.x, -v_Force);
    }


    #endregion


    #region Private Methods

    /// <summary>
    /// Take two vector2 positions for the line renderer component
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    void SetLinePositions(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

    /// <summary>
    /// Enable line renderer and aim it at a raycast2d hit point
    /// </summary>
    void AimLaser()
    {
        lineRenderer.enabled = true;

        var mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);


        if (mousePos.x < transform.position.x && facingRight)
        {
            Flip();
        }
        else if (mousePos.x > transform.position.x && !facingRight)
        {
            Flip();
        }


        if (Physics2D.Raycast(laserPoint.position, mousePos))
        {
            RaycastHit2D hit = Physics2D.Raycast(laserPoint.position, mousePos);
            SetLinePositions(laserPoint.position, hit.point);

            if (hit.collider.gameObject.tag == "Target")
            {
                targetTransform = hit.collider.transform;

                if (Input.GetMouseButtonDown(0))
                {
                    swapEvent.Invoke();
                    Vector2 tempPosition = transform.position;
                    transform.position = targetTransform.position;
                    hit.collider.transform.position = tempPosition;
                    targetTransform = null;
                }
            }
        }
        else
        {
            SetLinePositions(laserPoint.position, mousePos * defaultRayDistance);
        }
    }

    /// <summary>
    /// make the player face the direction they wish to fire the swazer
    /// </summary>
    void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1f;

        facingRight = !facingRight;

        gameObject.transform.localScale = newScale; 
    }

    /// <summary>
    /// Teleport to target transform, and reset value to null
    /// so player can't teleport without aiming at a valid target
    /// </summary>
    void Swap()
    {
        if (targetTransform != null)
        {
            swapEvent.Invoke();
            Vector2 tempPosition = transform.position;
            transform.position = targetTransform.position;
            targetTransform = null;
        }
    }

    #endregion
}
