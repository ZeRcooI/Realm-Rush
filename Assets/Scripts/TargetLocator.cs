using System;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] private Transform _weapon;
    private Transform _target;

    private void Start()
    {
        _target = FindObjectOfType<EnemyMover>().transform;
    }

    private void Update()
    {
        AimWeapon();
    }

    private void AimWeapon()
    {
        _weapon.LookAt(_target);
    }
}