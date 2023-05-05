using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class ColossusWalkState : ICharacterStates
{
    private Colossus _colossus;
    private int currentPatrolRoute=0;

    public ColossusWalkState(Colossus colossus)
    {
        _colossus = colossus;
    }

    public Type UpdateState()
    {
        _colossus.Animator.SetTrigger("Walk");
        _colossus.Agent.isStopped = false;

        if (_colossus.IsPatrol)
        {
            _colossus.Agent.destination = SetPatrolRoute();
            _colossus.IsPatrol = false;
            _colossus.NewState = null;
        }

        if (_colossus.transform.position == _colossus.Agent.destination)
        {
            _colossus.NewState = typeof(ColossusIdleState);
        }
        _colossus.IsDetected();
        
        if (_colossus.IsDamage) _colossus.NewState = typeof(ColossusDeathState);
        
        return  _colossus.NewState;
    }

    private Vector3 SetPatrolRoute()
    {
        int aux;
        
        do
        {
             aux=Random.Range(0, _colossus.PatrolPoints.Length);
            
        } while (aux == currentPatrolRoute);

        currentPatrolRoute = aux;
        
        return _colossus.PatrolPoints[currentPatrolRoute].position;
    }
}
