using UnityEngine.Tilemaps;

public class DiggingGameState : IGameState
{
    private Tilemap _tilemap;
    private WaypointPlacer _waypointPlacer;
    private Digger _digger;

    private MapRebuilder _mapRebuilder;

    public DiggingGameState(Tilemap tilemap, WaypointPlacer waypointPlacer, Digger digger)
    {
        _tilemap = tilemap;
        _waypointPlacer = waypointPlacer;
        _digger = digger;

        _mapRebuilder = new MapRebuilder(_tilemap);
    }

    public void Enter()
    {
        _mapRebuilder.CacheMap();
        _mapRebuilder.Rebuild();
        
        _waypointPlacer.Reset();
        _digger.Initialize(_waypointPlacer.LastWaypoint);
    }

    public void Exit() { }
}