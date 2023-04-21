using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class Bot : MonoBehaviour
{
    private NavMeshAgent _agent;
    private BotState _botState;
    private Animator _animator;
    private bool isPatrol;
    [SerializeField]private Transform[] patrolPoints;

    public Transform[] PatrolPoints => patrolPoints;
    
    public bool IsPatrol
    {
        get => isPatrol;
        set => isPatrol = value;
    }
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
        _botState = new BotState(this);
       
    }

    private void Update()
    {
        _botState.UpdateState();

    }





}
