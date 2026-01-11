using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DigRestrictor
{
    private Tilemap _tilemap;
    private TileBase _dugTile;

    private List<Vector3Int> _neighbourTilesPositions = new List<Vector3Int>()
    {
        new Vector3Int(-1, 0, 0),
        new Vector3Int(0, 1, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(0, -1, 0)
    };

    private int _neighboursDugLimitingCount = 2;
    private Vector3Int _lastDugTilePosition = Vector3Int.zero;

    public DigRestrictor(Tilemap tilemap, TileBase dugTile)
    {
        _tilemap = tilemap;
        _dugTile = dugTile;
    }

    public bool CanDig(Vector3Int digPosition)
    {
        TileBase diggableTile = _tilemap.GetTile(digPosition);

        if (diggableTile == null)
        {
            Debug.LogWarning($"Нельзя копать. Тайл на позиции {digPosition} отсутствует");
            return false;
        }

        if (diggableTile == _dugTile)
        {
            Debug.LogWarning($"Нельзя копать. Это происходит на уже вскопанном тайле");
            return false;
        }

        if (HasNearLastDugTile(digPosition) == false)
        {
            Debug.LogWarning($"Нельзя копать. Это происходит не рядом с последним вскопанным тайлом");
            return false;
        }

        int neighboursDugCount = GetNeighboursDugTilesCount(digPosition);

        if (neighboursDugCount >= _neighboursDugLimitingCount)
        {
            Debug.LogWarning($"Копать нельзя. У тайла {digPosition} больше 1го соседа");
            return false;
        }

        if (neighboursDugCount == 0)
        {
            Debug.LogWarning($"Копать нельзя. У тайла {digPosition} вообще нет соседей");
            return false;
        }

        _lastDugTilePosition = digPosition;

        return true;
    }

    private bool HasNearLastDugTile(Vector3Int digPosition)
    {
        if (_lastDugTilePosition == Vector3.zero)
        {
            return true;
        }

        foreach (var neighbourTilePosition in _neighbourTilesPositions)
        {
            Vector3Int checkableTilePosition = digPosition + neighbourTilePosition;

            if (checkableTilePosition == _lastDugTilePosition)
            {
                return true;
            }
        }

        return false;
    }

    private int GetNeighboursDugTilesCount(Vector3Int digPosition)
    {
        int neighboursDugCount = 0;

        foreach (var neighbourTilePosition in _neighbourTilesPositions)
        {
            Vector3Int checkableTilePosition = digPosition + neighbourTilePosition;

            if (_tilemap.GetTile(checkableTilePosition) == _dugTile)
            {
                neighboursDugCount++;
            }
        }

        return neighboursDugCount;
    }
}