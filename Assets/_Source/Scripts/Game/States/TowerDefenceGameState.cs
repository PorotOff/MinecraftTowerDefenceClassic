public class TowerDefenceGameState : IGameState
{
    private Digger _digger;
    private WaypointPlacer _waypointPlacer;

    public TowerDefenceGameState(Digger digger, WaypointPlacer waypointPlacer)
    {
        _digger = digger;
        _waypointPlacer = waypointPlacer;
    }

    public void Enter()
    {
        
    }

    public void Exit() { }
}