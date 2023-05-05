using System;
using UnityEngine;

public class ColossusHitState : ICharacterStates
{
    private Colossus _colossus;

    public ColossusHitState(Colossus colossus)
    {
       _colossus = colossus;
    }

    public Type UpdateState()
    {
      if (_colossus.Hit)
      {
         _colossus.Agent.isStopped = true;
         _colossus.Animator.SetTrigger("Punch");
         Collider[] objs = Physics.OverlapSphere(_colossus.DamageArea.position,_colossus.DamageRadius);
                
         foreach (var obj in objs)
         {
            if (obj.CompareTag("Player"))
            {
               var target = obj.gameObject.GetComponent<IDamageable>();
               target?.OnDamage(_colossus.PunchDamage);
               _colossus.Hit = false;
            }
         }
      }
      else
      {
         if (Time.time > _colossus.PunchRateTime)
         {
            _colossus.Animator.SetTrigger("Idle");
            _colossus.Hit = true;
            _colossus.PunchRateTime = Time.time + _colossus.PunchRate;
         }
      }
      

      if (Vector3.Distance(_colossus.transform.position, _colossus._iaSensor.Target.transform.position) > 8)
         _colossus.NewState = typeof(ColossusFollowState);
      
      if (!_colossus._iaSensor.Detected) _colossus.NewState = typeof(ColossusIdleState);

      if (_colossus.IsDamage) _colossus.NewState = typeof(ColossusDeathState);

      return _colossus.NewState;
    }
}
