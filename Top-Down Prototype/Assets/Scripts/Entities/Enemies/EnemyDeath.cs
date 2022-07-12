using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private int points;
    private Animator _animator;
    private EnemyWeaponHandler _weapon;
    private PointsAddedEvent pointsAddedEvent = new PointsAddedEvent();

    void Awake()
    {
        EventManager.AddHealthListener(HandleDeath);
        EventManager.AddPointsAddedInvoker(this);
    }

    private void HandleDeath(GameObject dyingObject)
    {
        _animator = dyingObject.GetComponent<Animator>();
        _weapon = dyingObject.GetComponent<EnemyWeaponHandler>();


        if (_weapon != null)
        {
            _weapon.enabled = false;
        }    
          

        if (_animator != null)
        {
            _animator?.SetTrigger("Dying");
        }

        pointsAddedEvent?.Invoke(points);
        Destroy(dyingObject, 1.0f);
        AudioManager.Play(AudioClipName.ZombieInmateDeath);
    }

    public void AddPointsAddedListener(UnityAction<int> _listener)
    {
        pointsAddedEvent.AddListener(_listener);
    }

    private void OnDestroy()
    {
        EventManager.RemoveOnDiedListener(HandleDeath);

    }
}
