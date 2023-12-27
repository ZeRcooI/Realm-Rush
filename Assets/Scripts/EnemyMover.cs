using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<Waypoint> _path = new List<Waypoint>();
    [SerializeField] private float _wayTime = 1f;

    private void Start()
    {
        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in _path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(_wayTime);
        }
    }
}