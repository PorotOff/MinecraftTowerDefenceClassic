using System;

public interface ITutorialState
{
    public event Action Finished;

    public void Enter();
    public void Exit();
}