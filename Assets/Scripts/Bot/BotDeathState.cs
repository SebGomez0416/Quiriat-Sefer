using System;
using UnityEngine;

public class BotDeathState : ICharacterStates
{
    private Bot _bot;
    private float time;
    
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
            _bot.Animator.SetTrigger("Death");
            _bot.Weapon.SetActive(false);
            _bot.NewState = null;
            Transform.Destroy(_bot.gameObject,5f);
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

}
