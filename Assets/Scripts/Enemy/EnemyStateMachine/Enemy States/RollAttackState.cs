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
        //Vector3 rollDirection = enemy.target.transform.position + (enemy.attackPostion.position - enemy.transform.position) / 2;
        PathRequestManager.RequestPath(enemy.transform.position, enemy.attackPostion.transform.position, enemy.OnRollPathFound); 
    }

    public override void Exit()
    {
        base.Exit();
        enemy.StopCoroutine(enemy.followPath);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!enemy.isPathfinding || enemy.isHit)
        {
            enemy.isHit = false;
            enemy.StopCoroutine(enemy.followPath);
            enemy.ChangeState(enemy.observeState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
