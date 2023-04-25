using System;
using UnityEngine;

public class BotIdleState : ICharacterStates
{
    private Bot _bot;
    private float time;
    
    public BotIdleState(Bot bot)
    {
        _bot = bot;
    }

    public Type UpdateState()
    {    
        _bot.Animator.SetTrigger("Idle");
        _bot.Weapon.SetActive(false);
        Timer();
        _bot.IsDetected();
        
        if (_bot.IsDamage) _bot.NewState = typeof(BotDeathState);

        return _bot.NewState;
    }

    private void Timer()
    {
        time += Time.deltaTime;
        if (!(time >= 4)) return;
        _bot.IsPatrol = true;
        _bot.NewState = typeof(BotWalkState);
        time = 0;
    }
}
