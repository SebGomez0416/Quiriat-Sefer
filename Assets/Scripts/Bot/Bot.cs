using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class Bot : MonoBehaviour,IDamageable,ISelectable
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
    [SerializeField] private Transform virtualCamera;
    [SerializeField] private ParticleSystem explosion;

    [Header("Settings")]
    [SerializeField] private float attackAngleSpeed;
    [SerializeField] private short life;
    [SerializeField] private float shotForce;
    [SerializeField] private float shotRate;
    private float maxLife;
    private float shotRateTime;
    private bool isDamage;
    private SkinnedMeshRenderer mat;
    private Color standart;
    [SerializeField] private Color colorless;
    [SerializeField] private Image lifeBar;
    [SerializeField] private GameObject _ui;

    public GameObject UI
    {
        get => _ui;
        set => _ui = value;
    }
    
    public float AttackAngleSpeed => attackAngleSpeed;
    
    public SkinnedMeshRenderer Mat
    {
        get => mat;
        set => mat = value;
    }
    public ParticleSystem Explosion
    {
        get => explosion;
        set => explosion = value;
    }
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
        mat = GetComponentInChildren<SkinnedMeshRenderer>();
        standart = mat.materials[1].color;
        mat.materials[1].color = colorless;
        maxLife = life;
    }

    private void Start()
    {
        _botState = new BotState(this);
    }

    private void Update()
    {
        _botState.UpdateState();
        iaSensor.UpdateSensor();
        UpdateLifeBar();
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

    public void SetSelection(bool state)
    {
        // activa y desactiva el OutLine
        mat.materials[1].color = state ? standart : colorless;
        
        //activa y desactiva la barra de vida
        _ui.SetActive(state);
    }

    private void UpdateLifeBar()
    {
        _ui.transform.LookAt(virtualCamera.position);
        lifeBar.fillAmount = life/maxLife;
    }
}
