public class AtBasePlayerState : IPlayerState
{
    private WaypointPlacer _waypointPlacer;
    private Digger _digger;

    public AtBasePlayerState(WaypointPlacer waypointPlacer, Digger digger)
    {
        _waypointPlacer = waypointPlacer;
        _digger = digger;
    }

    public void Enter()
    {
        _waypointPlacer.enabled = false;
        _digger.enabled = false;
    }

    public void Exit() { }
}