using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientPlayer : MonoBehaviour, IFlippable
{
    #region Fields
    
    private bool facingRight = true;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (mousePos.x < transform.position.x && facingRight)
        {
            Flip();
        }
        else if (mousePos.x > transform.position.x && !facingRight)
        {
            Flip();
        }
    }

    /// <summary>
    /// Have player character face the direction of the
    /// mouse cursor
    /// </summary>
    public virtual void Flip()
    {
        Vector3 newScale = gameObject.transform.localScale;
        newScale.x *= -1f;

        facingRight = !facingRight;

        gameObject.transform.localScale = newScale;
    }

}