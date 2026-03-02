using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Timer))]
public class GameStatesSwitcher : MonoBehaviour
{
    [SerializeField] private InputService _inputService;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private WaypointPlacer _waypointPlacer;
    [SerializeField] private Digger _digger;
    [SerializeField, Min(0)] private float _timerDurationSeconds = 50f;

    private Timer _timer;

    private IGameState _gameState;

    private void Awake()
    {
        _timer = GetComponent<Timer>();
    }

    private void Start()
    {
        OnRestarted();
    }

    private void OnEnable()
    {
        _inputService.Paused += OnPaused;
        _inputService.Continued += OnContinued;
        _inputService.Restarted += OnRestarted;

        _timer.TimeElapsed += OnTimeElapsed;
    }

    private void OnDisable()
    {
        _inputService.Paused -= OnPaused;
        _inputService.Continued -= OnContinued;
        _inputService.Restarted -= OnRestarted;
        
        _timer.TimeElapsed -= OnTimeElapsed;   
    }

    private void OnTimeElapsed()
    {
        SetState(new TowerDefenceGameState(_digger, _waypointPlacer));
    }

    private void OnPaused()
    {
        SetState(new PauseGameState());

        _timer.StopTimer();
    }

    private void OnContinued()
    {
        SetState(new ContinueGameState());

        _timer.Initialize(_timer.RemainingSeconds);
        _timer.StartTimer();
    }

    private void OnRestarted()
    {
        SetState(new StartGameState(_tilemap, _waypointPlacer, _digger));

        _timer.Initialize(_timerDurationSeconds);
        _timer.StartTimer();
    }

    private void SetState(IGameState gameState)
    {
        if (_gameState != null)
        {
            _gameState.Exit();

            if (_gameState == gameState)
                return;
        }

        _gameState = gameState;
        _gameState.Enter();

        Debug.Log($"Game state: {_gameState}");
    }
}