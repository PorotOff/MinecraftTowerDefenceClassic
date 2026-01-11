using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileConfig", menuName = "CONFIGURATIONS/Digging/TileConfig", order = 0)]
public class TileConfig : ScriptableObject
{
    [field: SerializeField] public TileBase TileBase { get; private set; }
    [field: SerializeField] public TilesTypes TileType { get; private set; }
}