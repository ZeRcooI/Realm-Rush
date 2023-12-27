using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<Waypoint> _path = new List<Waypoint>();
    [SerializeField][Range(0f, 5f)] private float _speed = 1f;

    private Enemy _enemy;

    private void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void FindPath()
    {
        _path.Clear();

        GameObject[] wayPoints = GameObject.FindGameObjectsWithTag("Path");

        foreach (GameObject wayPoint in wayPoints)
        {
            _path.Add(wayPoint.GetComponent<Waypoint>());
        }
    }

    private void ReturnToStart()
    {
        transform.position = _path[0].transform.position;
    }

    private IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in _path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPersent = 0f;

            transform.LookAt(endPosition);

            while (travelPersent < 1f)
            {
                travelPersent += Time.deltaTime * _speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPersent);

                yield return new WaitForEndOfFrame();
            }
        }

        _enemy.SteelGold();
        gameObject.SetActive(false);
    }
}