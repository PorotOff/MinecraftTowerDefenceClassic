using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Digger : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private TileBase _dugTile;
    [SerializeField] private bool _isHorizontalDigDirection = true;

    private InputService _inputService;

    private DigChecker _digChecker;

    private Waypoint _initialDigWaypoint;
    private bool _isHorizontalDigDirectionCached;

    public event Action Dug;
    public event Action DigDirectionChanged;

    public Vector3Int LastDugPosition { get; private set; }

    private void Awake()
    {
        _inputService = FindAnyObjectByType<InputService>(FindObjectsInactive.Include);
        _digChecker = new DigChecker(_tilemap, _dugTile);

        _isHorizontalDigDirectionCached = _isHorizontalDigDirection;
    }

    private void OnEnable()
    {
        _inputService.Pressed += Dig;
    }

    private void OnDisable()
    {
        _inputService.Pressed -= Dig;
    }

    public void Initialize(Waypoint initialDigWaypoint)
    {
        _initialDigWaypoint = initialDigWaypoint;
        LastDugPosition = _tilemap.WorldToCell(_initialDigWaypoint.transform.position);

        _isHorizontalDigDirection = _isHorizontalDigDirectionCached;
    }

    private void Dig()
    {
        Vector2 pressedScreenPointPosition = Camera.main.ScreenToWorldPoint(_inputService.PointerPosition);
        Vector3Int digPosition = _tilemap.WorldToCell(pressedScreenPointPosition);

        if (_digChecker.CanDig(digPosition, LastDugPosition) == false)
        {
            return;
        }

        if (_digChecker.IsDigDirectionChanged(_isHorizontalDigDirection, digPosition, LastDugPosition))
        {
            _isHorizontalDigDirection = !_isHorizontalDigDirection;
            DigDirectionChanged?.Invoke();
            // Debug.LogWarning($"Направление копания изменилось");
        }

        _tilemap.SetTile(digPosition, _dugTile);
        LastDugPosition = digPosition;
        Dug?.Invoke();
        // Debug.Log($"Вскопан тайл на позиции {digPosition}");

        if (_digChecker.IsAnyNextStepBlocked(LastDugPosition))
        {
            Debug.LogWarning($"Больше некуда копать");
        }
    }
}