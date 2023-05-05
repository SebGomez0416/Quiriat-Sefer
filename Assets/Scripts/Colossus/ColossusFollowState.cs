using System;
using UnityEngine;

public class ColossusFollowState : ICharacterStates
{
    private Colossus _colossus;
    private float PunchThreshold = 1;

    public ColossusFollowState(Colossus colossus)
    {
       _colossus = colossus;
    }

    public Type UpdateState()
    {
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

        if (Vector3.Angle(targetDirection, botDirection) < PunchThreshold)
        {
            _colossus.Animator.SetTrigger("Walk");
            _colossus.Agent.isStopped = false;
            _colossus.Agent.destination = _colossus._iaSensor.Target.position;
        }

        if (Vector3.Distance(_colossus.transform.position, _colossus._iaSensor.Target.transform.position) <= 6)
        {
            _colossus.NewState = typeof(ColossusHitState);
            _colossus.Hit = true;
        }
        
        if (!_colossus._iaSensor.Detected) _colossus.NewState = typeof(ColossusIdleState);
        
        if (_colossus.IsDamage) _colossus.NewState = typeof(ColossusDeathState);

        return _colossus.NewState;
    }

    

}
