using System;
using UnityEngine;

public class ColossusDeathState : ICharacterStates
{
    private Colossus _colossus;
    private float time;
    
    public ColossusDeathState(Colossus colossus)
    {
        _colossus = colossus;
    }
    public Type UpdateState()
    {
        _colossus.Agent.isStopped = true;
        Timer();
        
        if (_colossus.Life > 0)
        {
            _colossus.Animator.SetTrigger("Hurt");
        }
        else
        {
            _colossus.Animator.SetTrigger("Death");
            _colossus.NewState = null;
            Transform.Destroy(_colossus.gameObject,5f);
        }

        return _colossus.NewState;
    }
    
    private void Timer()
    {
        time += Time.deltaTime;
        if (!(time >= 1f)) return;
        _colossus.IsDamage = false;
        _colossus.NewState = typeof(ColossusIdleState);
        time = 0;
    }

}
