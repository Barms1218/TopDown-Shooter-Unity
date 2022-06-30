using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private void Awake() => Health.OnDied += HandleDeath;

    private void HandleDeath(GameObject dyingObject)
    {
        var dyingObjectMovement = dyingObject.GetComponent<EnemyMove>();
        var dyingObjectAnimator = dyingObject.GetComponent<Animator>();
        var dyingObjectCollider = dyingObject.GetComponent<Collider2D>();
        dyingObjectMovement.enabled = false;
        //GetComponent<MeleeAttack>().enabled = false;
        dyingObjectAnimator.SetTrigger("Dying");
        Debug.Log(dyingObject.name);
        //SpriteRenderer spriteRenderer =  dyingObject.GetComponent<SpriteRenderer>();

        //StartCoroutine(FadeOut(spriteRenderer, 1.0f));
        dyingObjectCollider.enabled = false;
        Destroy(dyingObject, 1.0f);

    }

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

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    private void OnDestroy()
    {
        Health.OnDied -= HandleDeath;
    }
}
