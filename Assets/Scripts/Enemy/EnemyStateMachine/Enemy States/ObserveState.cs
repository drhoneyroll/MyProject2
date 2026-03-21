using System.Collections;
using UnityEngine;

public class ObserveState : State
{
    public override void DoChecks()
        {
            base.DoChecks();           
        }

    public ObserveState(EnemyLogic _enemy, string _animBoolName) : base(_enemy, _animBoolName)
    {   
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Observing!");
        enemy.StartObserve();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
