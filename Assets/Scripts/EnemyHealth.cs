using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHitPoints = 5;
    private int _currentHitPoints = 0;

    private void Start()
    {
        _currentHitPoints = _maxHitPoints;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        _currentHitPoints--;

        if(_currentHitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}