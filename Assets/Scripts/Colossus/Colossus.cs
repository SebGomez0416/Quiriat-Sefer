using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Colossus : MonoBehaviour,IDamageable,ISelectable
{
    private NavMeshAgent _agent;
    private ColossusState _colossusState;
    private Animator _animator;
    private bool isPatrol;
    [SerializeField]private Transform[] patrolPoints;
    private IASensor iaSensor;
    private Type newState;
    private bool hit;
    [SerializeField] private Transform damageArea;
    [SerializeField] private Transform virtualCamera;

    [Header("Settings")]
    [SerializeField] private float attackAngleSpeed;
    [SerializeField] private float damageRadius;
    [SerializeField] private short life;
    [SerializeField] private short punchDamage;
    [SerializeField] private float punchRate;
    private float maxLife;
    private float punchRateTime;
    private bool isDamage;
    private SkinnedMeshRenderer mat;
    private Color standart;
    [SerializeField] private Color colorless;
    [SerializeField] private Image lifeBar;
    [SerializeField] private GameObject _ui;
    [SerializeField] private ParticleSystem explosion;
    public ParticleSystem Explosion
    {
        get => explosion;
        set => explosion = value;
    }
    public GameObject UI
    {
        get => _ui;
        set => _ui = value;
    }
    
    public SkinnedMeshRenderer Mat
    {
        get => mat;
        set => mat = value;
    }
    public float DamageRadius => damageRadius;
    public float AttackAngleSpeed => attackAngleSpeed;
    
    public bool Hit
    {
        get => hit;
        set => hit = value;
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

    public Transform DamageArea => damageArea;
    
    public short PunchDamage
    {
        get => punchDamage;
        set => punchDamage = value;
    }

    public float PunchRate
    {
        get => punchRate;
        set => punchRate = value;
    }

    public float PunchRateTime
    {
        get => punchRateTime;
        set => punchRateTime = value;
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
        _colossusState = new ColossusState(this);
    }

    private void Update()
    {
        _colossusState.UpdateState();
        iaSensor.UpdateSensor();
        UpdateLifeBar();
    }

    public void IsDetected()
    {
        if (iaSensor.Detected)
         newState = typeof(ColossusFollowState);
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
    
    private void OnDrawGizmos()
    {
        if (newState == typeof(ColossusHitState))
        {
            Gizmos.color= Color.red;
            Gizmos.DrawWireSphere(DamageArea.position,DamageRadius);
        }
    }
}
