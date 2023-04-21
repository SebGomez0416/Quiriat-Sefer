using System;
using System.Collections.Generic;
using UnityEngine;


public class BotState : MonoBehaviour
{
    
    public ICharacterStates CurrentState { get; private set; }
    
    private Dictionary<Type, ICharacterStates> _botStates;
    
    public BotState(Bot bot)
    {
        _botStates = new Dictionary<Type, ICharacterStates>()
        {
            { typeof(BotIdleState), new BotIdleState(bot) },
            { typeof(BotShootState), new BotShootState(bot) },
            { typeof(BotWalkState), new BotWalkState(bot) }
        };

        ChangeState(typeof(BotIdleState));
    }

    public void UpdateState()
    {
        Type newState = CurrentState.UpdateState();
        if(newState != null)
            ChangeState(newState);
    }
    
    private void ChangeState(Type newState)
    {
        CurrentState = _botStates[newState];
    }
    
}
