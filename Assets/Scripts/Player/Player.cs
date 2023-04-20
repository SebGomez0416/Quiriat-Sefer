using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Player : MonoBehaviour
{
    private NavMeshAgent _agent;
    private PlayerStates _playerState;
    private Animator _animator;
    private float walkSpeed;
    private const float DOUBLE_CLIK_TIME = 0.2F;
    private float lastCLickTime;
    private Type newState;

    public Type NewState
    {
        get => newState;
        set => newState = value;
    }

    public float WalkSpeed => walkSpeed;

    public float RunSpeed => runSpeed;

    private float runSpeed;

    public NavMeshAgent Agent
    {
        get => _agent;
        set => _agent = value;
    }
    
    public Animator Animator
    {
        get => _animator;
        set => _animator = value;
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _playerState = new PlayerStates(this);
        walkSpeed = _agent.speed;
        runSpeed = _agent.speed * 4.2f;
    }

    private void Update()
    {
       _playerState.UpdateState();

    }

    public void isDoubleClick()
    {
        float timeSinceLastClick = Time.time - lastCLickTime;
        
        if (timeSinceLastClick <= DOUBLE_CLIK_TIME)
        {
            Agent.speed = RunSpeed;
            newState= typeof(PlayerRunState);
        }
        else
        {
            Agent.speed = WalkSpeed;
            newState= typeof(PlayerWalkState);
        }

        lastCLickTime = Time.time;
    }

    

   

   
    
}
