using System;
using UnityEngine;

public class ColossusIdleState : ICharacterStates
{
    private Colossus _colossus;
    private float time;
    
    public ColossusIdleState(Colossus colossus)
    {
        _colossus = colossus;
    }

    public Type UpdateState()
    {    
        _colossus.Animator.SetTrigger("Idle");
        _colossus.Agent.isStopped = true;
        Timer();
        _colossus.IsDetected();
        
        if (_colossus.IsDamage) _colossus.NewState = typeof(ColossusDeathState);

        return _colossus.NewState;
    }

    private void Timer()
    {
        time += Time.deltaTime;
        if (!(time >= 2.0f)) return;
        _colossus.IsPatrol = true;
        _colossus.Agent.isStopped = false;
        _colossus.NewState = typeof(ColossusWalkState);
        time = 0;
    }
}
