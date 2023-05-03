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
        _turret.Barrel.transform.Rotate(Vector3.up,_turret.RotationAngle*Time.deltaTime);
        _turret.IsDetected();
        
        if(_turret.Life <=0)  _turret.NewState = typeof(TurretDeathState);
        
        return _turret.NewState;
    }
}
