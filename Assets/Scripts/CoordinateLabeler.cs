using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] private Color _defaultColor = Color.white;
    [SerializeField] private Color _blockedColor = Color.gray;

    private TextMeshPro _label;
    private Vector2Int _coordinates = new Vector2Int();
    private Waypoint _waypoint;

    private void Awake()
    {
        _label = GetComponent<TextMeshPro>();
        _label.enabled = false;
        _waypoint = GetComponentInParent<Waypoint>();
        DisolayCoordinates();
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            DisolayCoordinates();
            UpdateObjectName();
        }

        ColorCoordinates();
        ToggleLabels();
    }

    private void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _label.enabled = !_label.IsActive();
        }
    }

    private void ColorCoordinates()
    {
        if (_waypoint.IsPlaceable)
        {
            _label.color = _defaultColor;
        }
        else
        {
            _label.color = _blockedColor;
        }
    }

    private void DisolayCoordinates()
    {
        _coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        _coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        _label.text = $"{_coordinates.x},{_coordinates.y}";
    }

    private void UpdateObjectName()
    {
        transform.parent.name = _coordinates.ToString();
    }
}