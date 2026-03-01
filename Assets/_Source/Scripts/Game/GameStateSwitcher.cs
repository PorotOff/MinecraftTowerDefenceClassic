using UnityEngine;
using UnityEngine.Tilemaps;

public class GameStateSwitcher : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private InputService _inputService;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private WaypointPlacer _waypointPlacer;
    [SerializeField] private Digger _digger;

    [Header("Tower defence state")]
    [SerializeField] private TileBase _finishTile;

    private IGameState _gameState;

    private void Start()
    {
        SetState(new DiggingGameState(_tilemap, _waypointPlacer, _digger));
    }

    private void OnEnable()
    {
        _inputService.Restarted += Restart;
        _digger.Dug += StartTowerDefence;
    }

    private void OnDisable()
    {
        _inputService.Restarted -= Restart;
        _digger.Dug -= StartTowerDefence;
    }

    private void StartTowerDefence()
    {
        if (_digger.CurrentNeighbours.Contains(_finishTile))
        {
            SetState(new TowerDefenceGameState(_digger, _waypointPlacer));
        }
    }

    private void Restart()
    {
        SetState(new DiggingGameState(_tilemap, _waypointPlacer, _digger));
    }

    private void SetState(IGameState gameState)
    {
        if (_gameState != null)
        {
            _gameState.Exit();
        }

        _gameState = gameState;
        _gameState.Enter();

        Debug.Log($"Game state: {_gameState.GetType()}");
    }
}