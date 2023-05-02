using System;
using UnityEngine;

public class TurretShootState : ICharacterStates
{
    private Turret _turret;
    public TurretShootState(Turret turret)
    {
        _turret = turret;
    }

    public Type UpdateState()
    {
        throw new NotImplementedException();
    }
}
