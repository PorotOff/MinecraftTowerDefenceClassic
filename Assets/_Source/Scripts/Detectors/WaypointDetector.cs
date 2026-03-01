using UnityEngine;

public class WaypointDetector : ComponentDetector<Waypoint>
{
    public WaypointDetector(Transform transform, float detectionRadius) : base(transform, detectionRadius) { }
}