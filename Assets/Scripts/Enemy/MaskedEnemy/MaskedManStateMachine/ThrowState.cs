using UnityEngine;

public class ThrowState : GenericState<MaskedEnemyLogic>
{
    public override void DoChecks()
        {
            base.DoChecks();           
        }

    public ThrowState(MaskedEnemyLogic _enemy, string _animBoolName) : base(_enemy, _animBoolName)
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
