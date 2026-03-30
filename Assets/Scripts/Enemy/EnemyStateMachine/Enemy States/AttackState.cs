using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AttackState : State
{
    public override void DoChecks()
        {
            base.DoChecks();           
        }

    public AttackState(EnemyLogic _enemy, string _animBoolName) : base(_enemy, _animBoolName)
    {   
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Attacking!");
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
