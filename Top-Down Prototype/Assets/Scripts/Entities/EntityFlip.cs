using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFlip : MonoBehaviour
{
    bool facingRight = true;

    // Update is called once per frame
    void Update()
    {
        
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
}
