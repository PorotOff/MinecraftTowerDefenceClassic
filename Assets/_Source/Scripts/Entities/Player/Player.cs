using UnityEngine;

[RequireComponent(typeof(PlayerStatesService))]
public class Player : MonoBehaviour
{
    private PlayerStatesService _playerStatesService;

    private void Awake()
    {
        _playerStatesService = GetComponent<PlayerStatesService>();
    }

    private void Start()
    {
        Reset();
    }

    private void OnEnable()
    {
        _playerStatesService.DiggingFinished += _playerStatesService.SetAtBasePlayerState;
    }

    private void OnDisable()
    {
        _playerStatesService.DiggingFinished -= _playerStatesService.SetAtBasePlayerState;
    }

    public void Reset()
    {
        _playerStatesService.SetDiggingState();
    }
}