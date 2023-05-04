using System;
using UnityEngine;
using UnityEditor;

public class ColossusShootState : ICharacterStates
{
    private Colossus _colossus;
    private float shootingThreshold = 1;

    public ColossusShootState(Colossus colossus)
    {
       _colossus = colossus;
    }

    public Type UpdateState()
    {
        _colossus.Animator.SetTrigger("Shoot");

        Vector3 targetDirection = _colossus._iaSensor.Target.transform.position - _colossus.transform.position;
        targetDirection.y = 0;
        targetDirection = targetDirection.normalized;

        Vector3 botDirection = _colossus.transform.forward;
        botDirection = Vector3.RotateTowards(botDirection, targetDirection, _colossus.AttackAngleSpeed * Mathf.Deg2Rad * Time.deltaTime, 10.0f);

        Quaternion qDir = new Quaternion();
        qDir.SetLookRotation(botDirection,Vector3.up);
        _colossus.transform.rotation = qDir;

        _colossus.Agent.isStopped = true;
        _colossus.Agent.destination = _colossus.transform.position;

        if (Vector3.Angle(targetDirection, botDirection) < shootingThreshold)
        {
            if (Time.time > _colossus.ShotRateTime)
            {
                GameObject newBullet;
                newBullet = Editor.Instantiate(_colossus.Bullet, _colossus.SpawnPoint.position, _colossus.SpawnPoint.rotation);
                newBullet.GetComponent<Rigidbody>().AddForce(_colossus.SpawnPoint.forward*_colossus.ShotForce);
                _colossus.ShotRateTime = Time.time + _colossus.ShotRate;
                Editor.Destroy(newBullet,4);
            }
        }
        
        _colossus.NewState = typeof(ColossusIdleState);
        _colossus.IsDetected();
        
        if (_colossus.IsDamage) _colossus.NewState = typeof(ColossusDeathState);

        return _colossus.NewState;
    }

}
