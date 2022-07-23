using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private int points;
    public static UnityAction<int> GivePoints;
    public UnityEvent liveAgain;

    public void HandleDeath()
    {
                    
        if (TryGetComponent<Animator>(out Animator _animator))
        {
            _animator?.SetTrigger("Dying");
        }
        GivePoints?.Invoke(points);
        StartCoroutine(Die());
        AudioManager.Play(AudioClipName.ZombieInmateDeath);
    }

    private IEnumerator Die()
    {

        yield return new WaitForSeconds(1f);
        liveAgain.Invoke();
        gameObject.SetActive(false);
    }
}
