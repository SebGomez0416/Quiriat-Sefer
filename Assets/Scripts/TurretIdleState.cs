using System;
using UnityEngine;

public class TurretIdleState : ICharacterStates
{
    private Turret _turret;

    public TurretIdleState(Turret turret)
    {
        _turret = turret;
    }

    public Type UpdateState()
    {
        throw new NotImplementedException();
    }
}
