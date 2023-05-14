using System;
using UnityEngine;

public class BotDeathState : ICharacterStates
{
    private Bot _bot;
    private float time;
    private bool death;
    private float timeToExplosion;
    
    public static event Action OnDying;
    
    public BotDeathState(Bot bot)
    {
        _bot = bot;
    }
    public Type UpdateState()
    {
        _bot.Agent.isStopped = true;
        Timer();
        
        if (_bot.Life > 0)
        {
            _bot.Animator.SetTrigger("Hurt");
            _bot.Weapon.SetActive(true);
        }
        else
        {
            timeToExplosion += Time.deltaTime;
            _bot.Animator.SetTrigger("Death");
            ActiveExplosion();
           
            _bot.Weapon.SetActive(false);
            _bot.NewState = null;
            Transform.Destroy(_bot.gameObject,6f);
        }

        return _bot.NewState;
    }
    
    private void Timer()
    {
        time += Time.deltaTime;
        if (!(time >= 1f)) return;
        _bot.IsDamage = false;
        _bot.NewState = typeof(BotIdleState);
        time = 0;
    }

    private void ActiveExplosion()
    {
        if (timeToExplosion >= 2f && !death)
        {  
            death = true;
            _bot.Explosion.Play();
        }
        
        if (timeToExplosion >= 3f)
        {    
            OnDying?.Invoke();
            _bot.Mat.enabled = false;
            _bot.UI.SetActive(false);
            timeToExplosion = -10f;
        }
        
    }

}
