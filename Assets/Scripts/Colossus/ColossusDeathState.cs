using System;
using UnityEngine;

public class ColossusDeathState : ICharacterStates
{
    private Colossus _colossus;
    private float time;
    private bool death;
    private float timeToExplosion;
    
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
            timeToExplosion += Time.deltaTime;
            _colossus.Animator.SetTrigger("Death");
            ActiveExplosion();
            
            _colossus.NewState = null;
            Transform.Destroy(_colossus.gameObject,7f);
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
    
    private void ActiveExplosion()
    {
        if (timeToExplosion >= 3f && !death)
        {    
            death = true;
            _colossus.Explosion.Play();
        }

        if (!(timeToExplosion >= 4.4f)) return;
        timeToExplosion = 0;
        _colossus.Mat.enabled = false;
        _colossus.UI.SetActive(false);
    }

}
