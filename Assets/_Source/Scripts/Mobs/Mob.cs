using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WaypointDetector))]
public class Mob : MonoBehaviour, IPooledObject<Mob>
{
    private MobConfig _mobConfig;
    private List<Waypoint> _waypoints;

    private WaypointDetector _waypointDetector;

    private Follower _follower;

    private int _currentTargetWaypointIndex = 0;
    private Waypoint _currentTargetWaypoint;

    public event Action<Mob> Released;

    private void Awake()
    {
        _waypointDetector = GetComponent<WaypointDetector>();

        _follower = new Follower(transform);

        _currentTargetWaypoint = _waypoints[_currentTargetWaypointIndex];
    }

    private void Update()
    {
        _follower.Follow(_currentTargetWaypoint.transform.position, _mobConfig.Speed);
        
        if (_waypointDetector.IsReached(_currentTargetWaypoint.transform.position))
        {
            _currentTargetWaypointIndex++;
            _currentTargetWaypoint = _waypoints[_currentTargetWaypointIndex];
        }
    }

    public void Initialize(MobConfig mobConfig, List<Waypoint> waypoints)
    {
        _mobConfig = mobConfig;
        _waypoints = waypoints;
    }

    public void Release()
    {
        Released?.Invoke(this);
    }

    // todo Доделать скрипт. Сейчас можно получить ошибку, если моб дойдёт до последнего waypoint'а, то будет IndexOutOfRange
}