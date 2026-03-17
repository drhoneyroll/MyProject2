using System.Collections;
using UnityEngine;

public abstract class State 
{
    protected EnemyLogic enemy;

    protected float startTime;
    private string AnimBoolName;

    public State(EnemyLogic _enemy, string _animBoolName)
    {
        enemy = _enemy;
        AnimBoolName = _animBoolName;
    }
    public virtual void Exit()
    {
        enemy.animator.SetBool(AnimBoolName, false);
    }
    public virtual void Enter()
    {
        DoChecks();
        enemy.animator.SetBool(AnimBoolName, true);
        startTime = 0;
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }

}
