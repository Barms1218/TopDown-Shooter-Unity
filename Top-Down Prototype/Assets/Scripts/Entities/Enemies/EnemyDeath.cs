using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    private void Awake() => Health.OnDied += HandleDeath;

    private void HandleDeath()
    {
        if (GetComponent<IDamageable>().Health <= 0)
        {
            GetComponent<EnemyMove>().enabled = false;
            //GetComponent<MeleeAttack>().enabled = false;
            GetComponent<Animator>().SetTrigger("Dying");

            SpriteRenderer spriteRenderer =  gameObject.GetComponent<SpriteRenderer>();

            StartCoroutine(FadeOut(spriteRenderer, 1.0f));

            Destroy(gameObject, 1.0f);
        }

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
