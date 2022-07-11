using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private int points;
    private Animator _animator;
    private EnemyMove _movement;
    private EnemyWeaponHandler _weapon;
    public static UnityAction<int> addPoints;

    void Awake() => EventManager.AddHealthListener(HandleDeath);

    private void HandleDeath(GameObject dyingObject)
    {
        _animator = dyingObject.GetComponent<Animator>();
        _movement = dyingObject.GetComponent<EnemyMove>();
        _weapon = dyingObject.GetComponent<EnemyWeaponHandler>();


        if (_weapon != null)
        {
            _weapon.enabled = false;
        }    
        
  
        if (_movement != null)
        {
            _movement.enabled = false;
        }    

        if (_animator != null)
        {
            _animator?.SetTrigger("Dying");
        }    


        addPoints?.Invoke(points);
        Destroy(dyingObject, 1.0f);
        AudioManager.Play(AudioClipName.ZombieInmateDeath);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(HandleDeath);
    }
}
