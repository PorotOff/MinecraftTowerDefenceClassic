using System;
using UnityEngine.Tilemaps;

public class DiggingPlayerState : IPlayerState
{
    private WaypointPlacer _waypointPlacer;
    private Digger _digger;
    private TileBase _finishTile;

    public event Action DiggingFinished;

    public DiggingPlayerState(WaypointPlacer waypointPlacer, Digger digger, TileBase finishTile)
    {
        _waypointPlacer = waypointPlacer;
        _digger = digger;
        _finishTile = finishTile;
    }

    public void Enter()
    {
        _waypointPlacer.enabled = true;
        _digger.enabled = true;

        _digger.Dug += Tick;
    }

    public void Tick()
    {
        if (_digger.CurrentNeighbours.Contains(_finishTile))
        {
            DiggingFinished?.Invoke();
        }
    }

    public void Exit()
    {
        _digger.Dug -= Tick;
    }
}