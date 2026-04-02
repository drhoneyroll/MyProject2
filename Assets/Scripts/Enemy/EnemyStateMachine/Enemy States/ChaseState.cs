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
        enemy.StopFollowPath();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();   

        if (enemy.inRange)
        {
            enemy.ChangeState(enemy.observeState);
        }

        Vector3 delta = enemy.attackPostion.position - enemy.lastAttackPosition;

        if (delta.sqrMagnitude > enemy.sqrDistanceThreshold)
        {
            PathRequestManager.RequestPath(new PathRequest(enemy.transform.position, enemy.attackPostion.position, enemy.OnPathFound)); 
            enemy.lastAttackPosition = enemy.attackPostion.position;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }      

}

