using UnityEngine;

public class Follower
{
    private Transform _transform;

    public Follower(Transform transform)
    {
        _transform = transform;
    }

    public void Follow(Vector2 targetPosition, float speed)
    {
        _transform.position = Vector2.MoveTowards(_transform.position, targetPosition, speed * Time.deltaTime);
    }
}