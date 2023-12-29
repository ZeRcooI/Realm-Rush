using System;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int _startCoordinates;
    [SerializeField] Vector2Int _destinationCoordinates;

    private Node _startNode;
    private Node _destinationNode;
    private Node _currentSearchNode;

    private Queue<Node> _frontier = new Queue<Node>();
    private Dictionary<Vector2Int, Node> _reached = new Dictionary<Vector2Int, Node>();

    Vector2Int[] _direction = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

    private GridManager _gridManager;
    private Dictionary<Vector2Int, Node> _grid = new Dictionary<Vector2Int, Node>();

    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();

        if (_gridManager != null)
        {
            _grid = _gridManager.Grid;
        }
    }

    private void Start()
    {
        _startNode = _gridManager.Grid[_startCoordinates];
        _destinationNode = _gridManager.Grid[_destinationCoordinates];

        BreadthFirstSearch();
        BuildPath();
    }

    private void ExploreNeigbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach (Vector2Int direction in _direction)
        {
            Vector2Int neigborCoordinates = _currentSearchNode.coordinates + direction;

            if (_grid.ContainsKey(neigborCoordinates))
            {
                neighbors.Add(_grid[neigborCoordinates]);
            }
        }

        foreach (Node neighbor in neighbors)
        {
            if(!_reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connectedTo = _currentSearchNode;

                _reached.Add(neighbor.coordinates, neighbor);
                _frontier.Enqueue(neighbor);
            }
        }
    }

    private void BreadthFirstSearch()
    {
        bool isRunning = true;

        _frontier.Enqueue(_startNode);
        _reached.Add(_startCoordinates, _startNode);

        while(_frontier.Count > 0 && isRunning)
        {
            _currentSearchNode = _frontier.Dequeue();
            _currentSearchNode.isExplored = true;
            ExploreNeigbors();

            if(_currentSearchNode.coordinates == _destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    private List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();

        Node currentNode = _destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;

        while(currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();

        return path;
    }
}