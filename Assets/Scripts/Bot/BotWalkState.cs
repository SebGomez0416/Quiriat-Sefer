using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class BotWalkState : ICharacterStates
{
    private Bot _bot;
    private int currentPatrolRoute=0;

    public BotWalkState(Bot bot)
    {
        _bot = bot;
    }

    public Type UpdateState()
    {
        _bot.Animator.SetTrigger("Walk");
        _bot.Weapon.SetActive(false);
        _bot.Agent.isStopped = false;

        if (_bot.IsPatrol)
        {
            _bot.Agent.destination = SetPatrolRoute();
            _bot.IsPatrol = false;
            _bot.NewState = null;
        }

        if ( Vector3.Distance(_bot.transform.position , _bot.Agent.destination) <= 5)
        {
            _bot.Agent.destination = _bot.transform.position;
            _bot.NewState = typeof(BotIdleState);
        }
        _bot.IsDetected();
        
        if (_bot.IsDamage) _bot.NewState = typeof(BotDeathState);

        return  _bot.NewState;
    }

    private Vector3 SetPatrolRoute()
    {
        int aux;
        
        do
        {
           aux=Random.Range(0, _bot.PatrolPoints.Length);
            
        } while (aux == currentPatrolRoute);

        currentPatrolRoute = aux;
        
        return _bot.PatrolPoints[currentPatrolRoute].position;
    }
}
