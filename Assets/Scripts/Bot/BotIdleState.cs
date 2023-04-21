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
        Timer();

        return  _bot.IsPatrol? typeof(BotWalkState):null;
    }

    private void Timer()
    {
        time += Time.deltaTime;
        if (!(time >= 4)) return;
        _bot.IsPatrol = true;
        time = 0;
    }
}
