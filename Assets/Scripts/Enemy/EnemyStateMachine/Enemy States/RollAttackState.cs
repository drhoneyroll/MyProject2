using UnityEngine;

public class RollAttackState : State
{
        public override void DoChecks()
        {
            base.DoChecks();           
        }

    public RollAttackState(EnemyLogic _enemy, string _animBoolName) : base(_enemy, _animBoolName)
    {   
    }

    public override void Enter()
    {
        base.Enter();
        enemy.isPathfinding = true;
        Debug.Log("Roll Attack State");
        PathRequestManager.RequestPath(enemy.transform.position, enemy.attackPostion.transform.position, enemy.OnRollPathFound); 
    }

    public override void Exit()
    {
        base.Exit();
        enemy.StopFollowPath();
        enemy.isHit = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (enemy.isHit || !enemy.isPathfinding)
        {
            enemy.ChangeState(enemy.observeState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
