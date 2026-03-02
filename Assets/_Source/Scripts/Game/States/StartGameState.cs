using System;
using UnityEngine.Tilemaps;

public class StartGameState : IGameState
{
    private WaypointPlacer _waypointPlacer;
    private Digger _digger;

    private MapRebuilder _mapRebuilder;

    public StartGameState(Tilemap tilemap, WaypointPlacer waypointPlacer, Digger digger)
    {
        _waypointPlacer = waypointPlacer;
        _digger = digger;

        _mapRebuilder = new MapRebuilder(tilemap);
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