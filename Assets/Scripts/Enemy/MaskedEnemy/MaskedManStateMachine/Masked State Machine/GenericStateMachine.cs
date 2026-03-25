using UnityEngine;
using System.Collections;

public class GenericStateMachine : MonoBehaviour
{
    protected GenericState<MaskedEnemyLogic> CurrentState { get; private set; }

    public void Initilize(GenericState<MaskedEnemyLogic> state)
    {
        CurrentState = state;
        state.Enter();
    }

    public void ChangeState(GenericState<MaskedEnemyLogic> state)
    {
        CurrentState.Exit();
        CurrentState = state;
        state.Enter();
    }
}
