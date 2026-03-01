using System;
using UnityEngine;

public class ComponentDetector<T> : MonoBehaviour where T : MonoBehaviour
{
    private Transform _transform;
    private float _detectionRadius;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="detectionRadius">Указывать значение нужно, учитывая, что класс производит расчёты дистанции до объектов при помощи squMagnitude</param>
    public ComponentDetector(Transform transform, float detectionRadius)
    {
        _transform = transform;
        _detectionRadius = detectionRadius;
    }

    /// <summary>
    /// Расчитывает дистанцию до объекта, используя sqrMagnitude
    /// </summary>
    /// <param name="targetPosition"></param>
    public bool IsReached(Vector2 targetPosition)
    {
        float distance = (targetPosition - (Vector2)_transform.position).sqrMagnitude;

        if (distance < _detectionRadius)
        {
            return true;
        }

        return false;
    }
}