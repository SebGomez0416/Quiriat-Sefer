using System;
using System.Collections.Generic;
using UnityEngine;

public class ColossusState : MonoBehaviour
{
    public ICharacterStates CurrentState { get; private set; }
    
    private Dictionary<Type, ICharacterStates> _ColossusStates;
    
    public ColossusState(Colossus colossus)
    {
        _ColossusStates = new Dictionary<Type, ICharacterStates>()
        {
            { typeof(ColossusIdleState), new ColossusIdleState(colossus) },
            { typeof(ColossusHitState), new ColossusHitState(colossus) },
            { typeof(ColossusWalkState), new ColossusWalkState(colossus) },
            { typeof(ColossusDeathState), new ColossusDeathState(colossus) },
            { typeof(ColossusFollowState), new ColossusFollowState(colossus) }
        };

        ChangeState(typeof(ColossusIdleState));
    }

    public void UpdateState()
    {
        Type newState = CurrentState.UpdateState();
        if(newState != null)
            ChangeState(newState);
    }
    
    private void ChangeState(Type newState)
    {
        CurrentState = _ColossusStates[newState];
    }
}
