using System;
using UnityEngine.UI;
using UnityEngine;

public class Turret : MonoBehaviour,IDamageable,ISelectable
{
    private TurretState _turretState;
    private IASensor iaSensor;
    private Type newState;
    [SerializeField]private GameObject bullet;
    [SerializeField] private Transform spawnPoint;
    [SerializeField]private GameObject turret;
    [SerializeField]private GameObject barrel;
    [SerializeField] private Transform virtualCamera;


    [Header("Settings")]
    [SerializeField] private float rotationAngle;
    [SerializeField] private float attackAngleSpeed;
    [SerializeField] private short life;
    [SerializeField] private float shotForce;
    [SerializeField] private float shotRate;
    private float maxLife;
    private float shotRateTime;
    private bool isDamage;
    private MeshRenderer turretmat;
    private MeshRenderer barrelmat;
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
    
    public MeshRenderer TurretMat
    {
        get => turretmat;
        set => turretmat = value;
    }
    public MeshRenderer BarrelMat
    {
        get => barrelmat;
        set => barrelmat = value;
    }
    
    public float AttackAngleSpeed => attackAngleSpeed;
    public GameObject Bullet => bullet;
    public Transform SpawnPoint => spawnPoint;
    public float RotationAngle => rotationAngle;
    public float ShotForce => shotForce;
    public float ShotRate => shotRate;

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

    public GameObject Barrel
    {
        get => barrel;
        set => barrel = value;
    }

    public short Life
    {
        get => life;
        set => life = value;
    }

    public float ShotRateTime
    {
        get => shotRateTime;
        set => shotRateTime = value;
    }

    public bool IsDamage
    {
        get => isDamage;
        set => isDamage = value;
    }
    
    private void Awake()
    {
        iaSensor = GetComponent<IASensor>();
        turretmat = turret.GetComponent<MeshRenderer>();
        barrelmat = barrel.GetComponent<MeshRenderer>();
        
        standart =  turretmat.materials[1].color;
        turretmat.materials[1].color = colorless;
        barrelmat.materials[1].color = colorless;
        maxLife = life;
    }
    
    private void Start()
    {
        _turretState = new TurretState(this);
        DataBetweenScenes.instance.numberOfEnemies ++;
    }

    private void Update()
    {
        if (DataBetweenScenes.instance.isPaused) return;
        _turretState.UpdateState();
        iaSensor.UpdateSensor();
        UpdateLifeBar();
       
    }
    
    public void IsDetected()
    {
        newState = iaSensor.Detected ? typeof(TurretShootState) : typeof(TurretIdleState);
    }
    
    public void OnDamage(short damage)
    {
        life -= damage;
        isDamage = true;
    }

    public void SetSelection(bool state)
    {
        if (turretmat == null || barrelmat == null) return;
        // activa y desactiva el OutLine
        turretmat.materials[1].color = state ? standart : colorless;
        barrelmat.materials[1].color = state ? standart : colorless;
        
        //activa y desactiva la barra de vida
        _ui.SetActive(state);
    }

    private void UpdateLifeBar()
    {
        _ui.transform.LookAt(virtualCamera.position);
        lifeBar.fillAmount = life/maxLife;
    }
}
