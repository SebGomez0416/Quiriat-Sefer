using System;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class Bot : MonoBehaviour,IDamageable
{
    private NavMeshAgent _agent;
    private BotState _botState;
    private Animator _animator;
    private bool isPatrol;
    [SerializeField]private Transform[] patrolPoints;
    private IASensor iaSensor;
    private Type newState;
    [SerializeField]private GameObject weapon;
    [SerializeField]private GameObject bullet;
    [SerializeField] private Transform spawnPoint;

    [Header("Settings")]
    [SerializeField] private short life;
    [SerializeField] private float shotForce;
    [SerializeField] private float shotRate;
    private float shotRateTime;
    private bool isDamage;

    public short Life
    {
        get => life;
        set => life = value;
    }

    public bool IsDamage
    {
        get => isDamage;
        set => isDamage = value;
    }

    public GameObject Weapon => weapon;
    public GameObject Bullet => bullet;
    public Transform SpawnPoint => spawnPoint;
    
    public float ShotForce
    {
        get => shotForce;
        set => shotForce = value;
    }

    public float ShotRate
    {
        get => shotRate;
        set => shotRate = value;
    }

    public float ShotRateTime
    {
        get => shotRateTime;
        set => shotRateTime = value;
    }

    public IASensor _iaSensor
    {
        get => iaSensor;
        set => iaSensor = value;
    }
    
    public Type NewState
    {
        get => newState;
        set => newState = value;
    }
    
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
        _iaSensor = GetComponent<IASensor>();
    }

    private void Start()
    {
        _botState = new BotState(this);
    }

    private void Update()
    {
        _botState.UpdateState();
        iaSensor.UpdateSensor();
    }

    public void IsDetected()
    {
        if (iaSensor.Detected)
            newState = typeof(BotShootState);
    }
    
    public void OnDamage(short damage)
    {
        life -= damage;
        isDamage = true;
    }

}
