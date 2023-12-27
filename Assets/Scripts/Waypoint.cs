using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private GameObject _towerPrefab;
    [SerializeField] private bool _isPlaceable;

    public bool IsPlaceable => _isPlaceable;

    private void OnMouseDown()
    {
        if (_isPlaceable)
        {
            Instantiate(_towerPrefab, transform.position, Quaternion.identity);
            _isPlaceable = false;
        }
    }
}