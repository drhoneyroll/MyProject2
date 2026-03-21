using System.Collections;
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
        enemy.StopCoroutine(enemy.followPath);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();   
        if (Vector3.Distance(enemy.attackPostion.position, enemy.lastAttackPosition) > enemy.distanceThreshold)
        {
            PathRequestManager.RequestPath(enemy.transform.position, enemy.attackPostion.position, enemy.OnPathFound); 
            enemy.lastAttackPosition = enemy.attackPostion.position;
        }

        if (enemy.inRange)
        {
            enemy.ChangeState(enemy.observeState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }      

}

