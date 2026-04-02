using UnityEngine;

public class HitState : State
{
    public override void DoChecks()
        {
            base.DoChecks();           
        }

    public HitState(EnemyLogic _enemy, string _animBoolName) : base(_enemy, _animBoolName)
    {   
    }

    public override void Enter()
    {
        base.Enter();
        enemy.EnemyPushBackForce();
        enemy.isPathfinding = false;
        enemy.transform.gameObject.layer = LayerMask.NameToLayer("Default");
        enemy.HitStunOn();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.transform.gameObject.layer = LayerMask.NameToLayer("Attackable");
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
