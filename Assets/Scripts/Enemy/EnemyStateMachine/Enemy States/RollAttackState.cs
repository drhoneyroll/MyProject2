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
    
    float changeTimer = 1;

    public override void Enter()
    {
        base.Enter();
        enemy.isPathfinding = true;
        changeTimer = 1;
        Debug.Log("Roll Attack State");
        PathRequestManager.RequestPath(new PathRequest(enemy.transform.position, enemy.attackPostion.transform.position, enemy.OnRollPathFound)); 
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
            enemy.isPathfinding = false;
            enemy.ChangeState(enemy.observeState);
        }

        if(changeTimer < 0.1)
        {
            enemy.ChangeState(enemy.observeState);
        }

        changeTimer -= Time.deltaTime;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
