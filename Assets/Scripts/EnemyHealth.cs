using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHitPoints = 5;
    [SerializeField] private int _currentHitPoints = 0;

    private Enemy _enemy;

    private void OnEnable()
    {
        _currentHitPoints = _maxHitPoints;
    }

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
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
            gameObject.SetActive(false);
            _enemy.RewardGold();
        }
    }
}