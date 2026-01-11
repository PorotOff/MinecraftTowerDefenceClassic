using System;
using UnityEngine;

public class Waypoint : MonoBehaviour, IPooledObject<Waypoint>
{
    public event Action<Waypoint> Released;

    public void Release()
    {
        Released?.Invoke(this);
    }
}