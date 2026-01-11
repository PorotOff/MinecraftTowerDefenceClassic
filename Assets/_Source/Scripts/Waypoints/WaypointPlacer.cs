using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(WaypointsSpawner))]
public class WaypointPlacer : MonoBehaviour
{
    [SerializeField, Tooltip("Располагать waypoint'ы в соответствии с их порядком")] private List<Waypoint> _cachedWaypoints = new List<Waypoint>();
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Digger _digger;

    private WaypointsSpawner _waypointsSpawner;

    private Vector3 _placementOffset = new Vector3(0.5f, 0.5f, 0f);
    private List<Waypoint> _waypoints = new List<Waypoint>();

    public Waypoint LastWaypoint { get; private set; }

    private void Awake()
    {
        _waypointsSpawner = GetComponent<WaypointsSpawner>();

        Reset();
    }

    private void OnEnable()
    {
        _digger.Dug += OnDiggerDug;
        _digger.DigDirectionChanged += OnDiggerDigDirectionChanged;
    }

    private void OnDisable()
    {
        _digger.Dug -= OnDiggerDug;
        _digger.DigDirectionChanged -= OnDiggerDigDirectionChanged;
    }

    public void Reset()
    {
        EnableCachedWaypoints();

        _waypointsSpawner.ReleaseAll();
        _waypoints.Clear();

        CreateCachedBasedWaypoints();

        LastWaypoint = _waypoints[_waypoints.Count - 1];

        DisableCachedWaypoints();
    }

    private void EnableCachedWaypoints()
    {
        foreach (var cachedWaypoint in _cachedWaypoints)
        {
            cachedWaypoint.gameObject.SetActive(true);
        }
    }

    private void DisableCachedWaypoints()
    {
        foreach (var cachedWaypoint in _cachedWaypoints)
        {
            cachedWaypoint.gameObject.SetActive(false);
        }
    }

    private void CreateCachedBasedWaypoints()
    {
        foreach (var cachedWaypoint in _cachedWaypoints)
        {
            Waypoint waypoint = _waypointsSpawner.Spawn();
            waypoint.transform.position = cachedWaypoint.transform.position;

            _waypoints.Add(waypoint);
        }
    }

    private void OnDiggerDug()
    {
        Vector3 placementPosition = _tilemap.CellToWorld(_digger.LastDugPosition);
        LastWaypoint.transform.position = placementPosition + _placementOffset;
    }

    private void OnDiggerDigDirectionChanged()
    {
        LastWaypoint = _waypointsSpawner.Spawn();
        _waypoints.Add(LastWaypoint);
    }
}