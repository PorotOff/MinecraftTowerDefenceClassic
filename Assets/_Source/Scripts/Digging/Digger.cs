using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Digger : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private TileBase _dugTile;

    private InputService _inputService;

    private DigRestrictor _digRestrictor;

    public event Action Dug;

    private void Awake()
    {
        _inputService = FindAnyObjectByType<InputService>(FindObjectsInactive.Include);
        _digRestrictor = new DigRestrictor(_tilemap, _dugTile);
    }

    private void OnEnable()
    {
        _inputService.Pressed += OnPressedInputService;
    }

    private void OnDisable()
    {
        _inputService.Pressed -= OnPressedInputService;
    }

    private void OnPressedInputService()
    {
        Vector2 pressedScreenPointPosition = Camera.main.ScreenToWorldPoint(_inputService.PointerPosition);
        Vector3Int pressedTilePosition = _tilemap.WorldToCell(pressedScreenPointPosition);

        if (_digRestrictor.CanDig(pressedTilePosition))
        {
            _tilemap.SetTile(pressedTilePosition, _dugTile);
            Dug?.Invoke();
            Debug.Log($"Вскопан тайл на позиции {pressedTilePosition}");
        }
    }
}