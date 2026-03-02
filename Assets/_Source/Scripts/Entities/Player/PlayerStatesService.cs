using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerStatesService : MonoBehaviour
{
    [SerializeField] private WaypointPlacer _waypointPlacer;
    [SerializeField] private Digger _digger;
    [SerializeField] private TileBase _finishTile;

    private IPlayerState _playerState;

    public event Action DiggingFinished;

    public void SetDiggingState()
    {
        DiggingPlayerState diggingPlayerState = new DiggingPlayerState(_waypointPlacer, _digger, _finishTile);
        diggingPlayerState.DiggingFinished += OnDiggingFinished;

        SetState(diggingPlayerState);
    }

    public void SetAtBasePlayerState()
    {
        SetState(new AtBasePlayerState(_waypointPlacer, _digger));
    }

    private void OnDiggingFinished()
    {
        if (_playerState is DiggingPlayerState diggingPlayerState)
        {
            diggingPlayerState.DiggingFinished -= OnDiggingFinished;
        }

        DiggingFinished?.Invoke();
    }

    private void SetState(IPlayerState playerState)
    {
        if (_playerState != null)
        {
            _playerState.Exit();

            if (_playerState == playerState)
                return;
        }

        _playerState = playerState;
        _playerState.Enter();

        Debug.Log($"Player state: {_playerState}");
    }
}