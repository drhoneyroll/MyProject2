using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected State CurrentState { get; private set; }

    public void Initilize(State state)
    {
        CurrentState = state;
        state.Enter();
    }

    public void ChangeState(State state)
    {
        CurrentState.Exit();
        CurrentState = state;
        state.Enter();
    }
}
