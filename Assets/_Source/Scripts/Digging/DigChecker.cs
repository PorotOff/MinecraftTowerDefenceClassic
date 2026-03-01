using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DigChecker
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

    public DigChecker(Tilemap tilemap, TileBase dugTile)
    {
        _tilemap = tilemap;
        _dugTile = dugTile;
    }

    public bool IsAnyNextStepBlocked(Vector3Int lastDugPosition)
    {
        foreach (var neighbourTilePosition in _neighbourTilesPositions)
        {
            Vector3Int checkableTilePosition = lastDugPosition + neighbourTilePosition;

            if (CanDig(checkableTilePosition, lastDugPosition) == true)
            {
                return false;
            }
        }

        return true;
    }

    public bool CanDig(Vector3Int currentDigPosition, Vector3Int lastDugPosition)
    {
        TileBase diggableTile = _tilemap.GetTile(currentDigPosition);

        if (diggableTile == null)
        {
            // Debug.LogWarning($"Нельзя копать. Тайл на позиции {currentDigPosition} отсутствует");
            return false;
        }

        if (diggableTile == _dugTile)
        {
            // Debug.LogWarning($"Нельзя копать. Это происходит на уже вскопанном тайле");
            return false;
        }

        if (HasNearLastDugTile(currentDigPosition, lastDugPosition) == false)
        {
            // Debug.LogWarning($"Нельзя копать. Это происходит не рядом с последним вскопанным тайлом");
            return false;
        }

        int neighboursDugCount = GetNeighboursDugTilesCount(currentDigPosition);

        if (neighboursDugCount >= _neighboursDugLimitingCount)
        {
            // Debug.LogWarning($"Копать нельзя. У тайла {currentDigPosition} больше 1го соседа");
            return false;
        }

        if (neighboursDugCount == 0)
        {
            // Debug.LogWarning($"Копать нельзя. У тайла {currentDigPosition} вообще нет соседей");
            return false;
        }

        return true;
    }

    public bool IsDigDirectionChanged(bool isHorizontalDigDirection, Vector3Int currentDigPosition, Vector3Int lastDugPosition)
    {
        if (isHorizontalDigDirection && lastDugPosition.y != currentDigPosition.y)
        {
            return true;
        }

        if (isHorizontalDigDirection == false && lastDugPosition.x != currentDigPosition.x)
        {
            return true;
        }

        return false;
    }

    public List<TileBase> GetNeighboursTiles(Vector3Int currentPosition)
    {
        List<TileBase> tiles = new List<TileBase>();

        foreach (var neighbourTilePosition in _neighbourTilesPositions)
        {
            TileBase neighbourTile = _tilemap.GetTile(currentPosition + neighbourTilePosition);
            
            if (neighbourTile != null)
            {
                tiles.Add(neighbourTile);
            }
        }

        return tiles;
    }

    private bool HasNearLastDugTile(Vector3Int digPosition, Vector3Int lastDugPosition)
    {
        foreach (var neighbourTilePosition in _neighbourTilesPositions)
        {
            Vector3Int checkableTilePosition = digPosition + neighbourTilePosition;

            if (checkableTilePosition == lastDugPosition)
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