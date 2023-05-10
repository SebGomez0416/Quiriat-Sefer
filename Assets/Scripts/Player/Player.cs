using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Player : MonoBehaviour,IDamageable
{
    private NavMeshAgent _agent;
    private PlayerStates _playerState;
    private Animator _animator;
    private float walkSpeed;
    private const float DOUBLE_CLIK_TIME = 0.2F;
    private float lastCLickTime;
    private Type newState;
    [SerializeField]private GameObject weapon;
    [SerializeField]private GameObject weaponBack;
    [SerializeField]private GameObject bullet;
    [SerializeField] private Transform spawnPoint;
    private bool isShoot;

    [Header("Settings")] 
    [SerializeField] private short life;
    [SerializeField] private float shotForce;
    [SerializeField] private float shotRate;
    private float shotRateTime;
    private GameObject enemyPosition;
    private bool isDamage;
    private ISelectable obj;
    
    public GameObject EnemyPosition => enemyPosition;
    public GameObject Bullet => bullet;
    public Transform SpawnPoint => spawnPoint;
    
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
    
    public bool _isShoot
    {
        get => isShoot;
        set => isShoot = value;
    }
    
    public GameObject Weapon
    {
        get => weapon;
        set => weapon = value;
    }
    
    public GameObject WeaponBack
    {
        get => weaponBack;
        set => weaponBack = value;
    }

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
        runSpeed = _agent.speed * 5f;
    }

    private void Update()
    {
       _playerState.UpdateState();

    }

    public void IsDoubleClick()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray,out hit))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                   enemyPosition = hit.transform.gameObject;
                   obj = enemyPosition.GetComponent<ISelectable>();
                   obj?.SetSelection(true);
                }
                else
                {
                    obj?.SetSelection(false);
                    enemyPosition = null;
                    obj = null;
                    Agent.destination = hit.point;
                }
            }
                

            float timeSinceLastClick = Time.time - lastCLickTime;

            if (timeSinceLastClick <= DOUBLE_CLIK_TIME)
            {
                Agent.speed = RunSpeed;
                newState = typeof(PlayerRunState);
            }
            else
            {
                Agent.speed = WalkSpeed;
                newState = typeof(PlayerWalkState);
            }

            lastCLickTime = Time.time;

        }
    }

    public void IsShoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isShoot = true;
            Agent.isStopped = true;
            Agent.destination = transform.position;
            NewState = typeof(PlayerShootState);
        }
        else
        {
            _isShoot = false;
            Agent.isStopped = false;
        }
    }
    
    public void OnDamage(short damage)
    {
        life -= damage;
        isDamage = true;
    }
}

