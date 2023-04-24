using System;

using UnityEngine;
using UnityEditor;

public class BotShootState : ICharacterStates
{
    private Bot _bot;

    public BotShootState(Bot bot)
    {
       _bot = bot;
    }

    public Type UpdateState()
    {
        _bot.Animator.SetTrigger("Shoot");
        _bot.Weapon.SetActive(true);

        _bot.transform.forward=_bot._iaSensor.Target.position- _bot.transform.position;

        _bot.Agent.isStopped = true;
        _bot.Agent.destination = _bot.transform.position;
        
        if (Time.time > _bot.ShotRateTime)
        {
            GameObject newBullet;
            newBullet = Editor.Instantiate(_bot.Bullet, _bot.SpawnPoint.position, _bot.SpawnPoint.rotation);
            newBullet.GetComponent<Rigidbody>().AddForce(_bot.SpawnPoint.forward*_bot.ShotForce);
            _bot.ShotRateTime = Time.time + _bot.ShotRate;
            Editor.Destroy(newBullet,4);
        }

        _bot.NewState = typeof(BotIdleState);
        _bot.IsDetected();

        return _bot.NewState;
    }

}
