using System;
using UnityEngine;
using UnityEditor;

public class TurretShootState : ICharacterStates
{
    private Turret _turret;
    private float shootingThreshold = 1;
    public TurretShootState(Turret turret)
    {
        _turret = turret;
    }

    public Type UpdateState()
    {
        Vector3 targetDirection = _turret._iaSensor.Target.transform.position - _turret.transform.position;
        targetDirection.y = 0;
        targetDirection = targetDirection.normalized;

        Vector3 barrelDirection = _turret.Barrel.transform.forward;
        barrelDirection = Vector3.RotateTowards(barrelDirection, targetDirection, _turret.AttackAngleSpeed * Mathf.Deg2Rad * Time.deltaTime, 10.0f);

        Quaternion qDir = new Quaternion();
        qDir.SetLookRotation(barrelDirection,Vector3.up);
        _turret.Barrel.transform.rotation = qDir;


        if (Vector3.Angle(targetDirection, barrelDirection) < shootingThreshold)
        {
            if (Time.time > _turret.ShotRateTime)
            {
                GameObject newBullet;
                newBullet = MonoBehaviour.Instantiate(_turret.Bullet, _turret.SpawnPoint.position, _turret.SpawnPoint.rotation);
                newBullet.GetComponent<Rigidbody>().AddForce(_turret.SpawnPoint.forward*_turret.ShotForce);
                _turret.ShotRateTime = Time.time + _turret.ShotRate;
                Transform.Destroy(newBullet,4);
            }
        }

        _turret.NewState = typeof(TurretIdleState);
        _turret.IsDetected();
        
        if(_turret.Life <=0)  _turret.NewState = typeof(TurretDeathState);

        return _turret.NewState;
    }
}
