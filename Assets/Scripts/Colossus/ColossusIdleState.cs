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
        Timer();
        _colossus.IsDetected();
        
        if (_colossus.IsDamage) _colossus.NewState = typeof(ColossusDeathState);

        return _colossus.NewState;
    }

    private void Timer()
    {
        time += Time.deltaTime;
        if (!(time >= 5.4f)) return;
        _colossus.IsPatrol = true;
        _colossus.NewState = typeof(ColossusWalkState);
        time = 0;
    }
}
