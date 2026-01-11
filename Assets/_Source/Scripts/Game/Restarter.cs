using UnityEngine;
using UnityEngine.Tilemaps;

public class Restarter : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private InputService _inputService;
    [SerializeField] private WaypointPlacer _waypointPlacer;
    [SerializeField] private Digger _digger;

    private MapRebuilder _mapRebuilder;

    private void Awake()
    {
        _mapRebuilder = new MapRebuilder(_tilemap);
        
        _mapRebuilder.CacheMap();
    }

    private void Start()
    {
        Restart();
    }

    private void OnEnable()
    {
        _inputService.Restarted += Restart;
    }

    private void OnDisable()
    {
        _inputService.Restarted -= Restart;
    }

    private void Restart()
    {
        _mapRebuilder.Rebuild();
        _waypointPlacer.Reset();
        _digger.Initialize(_waypointPlacer.LastWaypoint);
    }
}