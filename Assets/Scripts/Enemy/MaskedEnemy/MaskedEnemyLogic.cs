using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class MaskedEnemyLogic : Enemy
{

    public AimState aimState;
    public ThrowState throwState;
    public Masked_ChaseState chaseState;
    public Masked_HitState hitState;

    void Awake()
    {
        aimState = new AimState(this, "aim");
        throwState = new ThrowState(this, "throw");
        chaseState = new  Masked_ChaseState(this, "run");
        hitState = new Masked_HitState(this, "hit");
    }

    void Start()
    {
    }


}
