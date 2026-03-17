using UnityEngine;

public class ChaseState : State
{
    public override void DoChecks()
        {
            base.DoChecks();           
        }

    public ChaseState(EnemyLogic _enemy, string _animBoolName) : base(_enemy, _animBoolName)
    {   
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Chasing!");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();   
        PathRequestManager.RequestPath(enemy.transform.position, enemy.target.position, enemy.OnPathFound); 
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}

