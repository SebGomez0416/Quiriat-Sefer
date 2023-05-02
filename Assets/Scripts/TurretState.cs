using System;
using System.Collections.Generic;
using UnityEngine;

public class TurretState : MonoBehaviour
{
    public ICharacterStates CurrentState { get; private set; }
   
    private Dictionary<Type, ICharacterStates> _turretStates;
   
    public TurretState(Turret turret)
    {
        _turretStates = new Dictionary<Type, ICharacterStates>()
        {
            { typeof(TurretIdleState), new TurretIdleState(turret)},
            { typeof(TurretShootState), new TurretShootState(turret)}
            //{ typeof(TurretDeathState), new PlayerDeathState(turret) }
        };
      
        ChangeState(typeof(PlayerIdleState));
    }

    public void UpdateState()
    {
        Type newState = CurrentState.UpdateState();
      
        if (newState != null)
            ChangeState(newState);
    }
   
    private void ChangeState(Type newState)
    {
        CurrentState = _turretStates[newState];
    }
}
