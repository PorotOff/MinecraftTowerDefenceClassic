using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapRebuilder
{
    private Tilemap _tilemap;

    private Dictionary<Vector3Int, TileBase> _map = new Dictionary<Vector3Int, TileBase>();

    public MapRebuilder(Tilemap tilemap)
    {
        _tilemap = tilemap;
    }

    public void Rebuild()
    {
        _tilemap.ClearAllTiles();

        foreach(var tile in _map)
        {
            _tilemap.SetTile(tile.Key, tile.Value);
        }
    }

    public void CacheMap()
    {
        foreach (var tilePosition in _tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = _tilemap.GetTile(tilePosition);

            if (tile != null)
            {
                _map[tilePosition] = tile;
            }
        }
    }
}