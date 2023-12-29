using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Tower _towerPrefab;
    [SerializeField] private bool _isPlaceable;

    private GridManager _gridManager;
    private Pathfinder _pathfinder;
    private Vector2Int _coordinates = new Vector2Int();

    public bool IsPlaceable => _isPlaceable;

    private void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
        _pathfinder = FindObjectOfType<Pathfinder>();
    }

    private void Start()
    {
        if(_gridManager != null)
        {
            _coordinates = _gridManager.GetCoordinatesFromPosition(transform.position);

            if(!_isPlaceable)
            {
                _gridManager.BlockNode(_coordinates);
            }
        }
    }

    private void OnMouseDown()
    {
        if (_gridManager.GetNode(_coordinates).isWalkable && !_pathfinder.WillBlockPath(_coordinates))
        {
             bool isPlaced = _towerPrefab.CreateTower(_towerPrefab, transform.position);

            _isPlaceable = !isPlaced;

            _gridManager.BlockNode(_coordinates);
        }
    }
}