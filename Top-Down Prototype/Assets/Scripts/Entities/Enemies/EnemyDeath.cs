using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private int points;
    public UnityEvent<int> givePoints;

    public void HandleDeath()
    {
                    
        if (TryGetComponent<Animator>(out Animator _animator))
        {
            _animator?.SetTrigger("Dying");
        }
        givePoints.Invoke(points);
        Destroy(gameObject, 1.0f);
        AudioManager.Play(AudioClipName.ZombieInmateDeath);
    }
}
