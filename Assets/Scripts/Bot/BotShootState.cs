using System;
using UnityEngine;
using UnityEditor;

public class BotShootState : ICharacterStates
{
    private Bot _bot;
    private float shootingThreshold = 1;

    public BotShootState(Bot bot)
    {
       _bot = bot;
    }

    public Type UpdateState()
    {
        _bot.Animator.SetTrigger("Shoot");
        _bot.Weapon.SetActive(true);

        Vector3 targetDirection = _bot._iaSensor.Target.transform.position - _bot.transform.position;
        targetDirection.y = 0;
        targetDirection = targetDirection.normalized;

        Vector3 botDirection = _bot.transform.forward;
        botDirection = Vector3.RotateTowards(botDirection, targetDirection, _bot.AttackAngleSpeed * Mathf.Deg2Rad * Time.deltaTime, 10.0f);

        Quaternion qDir = new Quaternion();
        qDir.SetLookRotation(botDirection,Vector3.up);
        _bot.transform.rotation = qDir;

        _bot.Agent.isStopped = true;
        _bot.Agent.destination = _bot.transform.position;

        if (Vector3.Angle(targetDirection, botDirection) < shootingThreshold)
        {
            if (Time.time > _bot.ShotRateTime)
            {
                GameObject newBullet;
                newBullet = MonoBehaviour.Instantiate(_bot.Bullet, _bot.SpawnPoint.position, _bot.SpawnPoint.rotation);
                newBullet.GetComponent<Rigidbody>().AddForce(_bot.SpawnPoint.forward*_bot.ShotForce);
                _bot.ShotRateTime = Time.time + _bot.ShotRate;
                Transform.Destroy(newBullet,4);
            }
        }
        
        _bot.NewState = typeof(BotIdleState);
        _bot.IsDetected();
        
        if (_bot.IsDamage) _bot.NewState = typeof(BotDeathState);

        return _bot.NewState;
    }

}
