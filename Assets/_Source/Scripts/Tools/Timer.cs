using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField, Min(0)] float _waitSeconds = 1f;
    [SerializeField, Min(0)] float _durationSeconds = 10f;

    private Coroutine _coroutine;

    public event Action Ticked;
    public event Action TimeElapsed;

    public float RemainingSeconds { get; private set; }

    public void Initialize(float durationSeconds)
    {
        _durationSeconds = durationSeconds;
    }

    public void StartTimer()
    {
        StopTimer();
        _coroutine = StartCoroutine(Work());
    }

    public void StopTimer()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private IEnumerator Work()
    {
        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(_waitSeconds);
        RemainingSeconds = _durationSeconds;

        while (enabled && RemainingSeconds > 0)
        {
            Ticked?.Invoke();

            yield return wait;
            RemainingSeconds -= _waitSeconds;
        }

        TimeElapsed?.Invoke();
    }
}