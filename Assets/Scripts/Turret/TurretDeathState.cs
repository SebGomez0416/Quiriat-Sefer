using System;
using UnityEngine;

public class TurretDeathState : ICharacterStates
{
    private Turret _turret;

    public TurretDeathState(Turret turret)
    {
        _turret = turret;
    }

    public Type UpdateState()
    {
        _turret.NewState = null;
        Transform.Destroy(_turret.gameObject,5f);

        return _turret.NewState;
    }

}
