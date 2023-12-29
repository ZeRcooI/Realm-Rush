using System;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Vector2Int _gridSize;

    [Tooltip("World Gris Size - Should match UnityEditor snap setting.")]
    [SerializeField] private int _unityGridSize = 10;

    private Dictionary<Vector2Int, Node> _grid = new Dictionary<Vector2Int, Node>();

    public Dictionary<Vector2Int, Node> Grid => _grid;

    public int UnityGridSize  => _unityGridSize;

    private void Awake()
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int coordinates)
    {
        if (_grid.ContainsKey(coordinates))
        {
            return _grid[coordinates];
        }

        return null;
    }

    public void BlockNode(Vector2Int coordinates)
    {
        if(_grid.ContainsKey(coordinates))
        {
            _grid[coordinates].isWalkable = false;
        }
    }

    public void ResetNodes()
    {
        foreach (KeyValuePair<Vector2Int, Node> entry in _grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();

        coordinates.x = Mathf.RoundToInt(position.x / _unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / _unityGridSize);

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();

        position.x = coordinates.x * _unityGridSize;
        position.z = coordinates.x * _unityGridSize;

        return position;
    }

    private void CreateGrid()
    {
        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);

                _grid.Add(coordinates, new Node(coordinates, true));
            }
        }
    }
}