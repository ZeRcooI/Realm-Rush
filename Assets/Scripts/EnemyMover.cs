using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField][Range(0f, 5f)] private float _speed = 1f;

    private List<Node> _path = new List<Node>();
    private Enemy _enemy;

    private GridManager _gridManager;
    private Pathfinder _pathFinder;

    private void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _gridManager = GetComponent<GridManager>();
        _pathFinder = GetComponent<Pathfinder>();
    }

    private void FindPath()
    {
        _path.Clear();

        _path = _pathFinder.GetNewPath();
    }

    private void ReturnToStart()
    {
        transform.position = _gridManager.GetPositionFromCoordinates(_pathFinder.StartCoordinates);
    }

    private void FinishPath()
    {
        _enemy.SteelGold();
        gameObject.SetActive(false);
    }

    private IEnumerator FollowPath()
    {
        for (int i = 0; i < _path.Count; i ++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = _gridManager.GetPositionFromCoordinates(_path[i].coordinates);
            float travelPersent = 0f;

            transform.LookAt(endPosition);

            while (travelPersent < 1f)
            {
                travelPersent += Time.deltaTime * _speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPersent);

                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }
}