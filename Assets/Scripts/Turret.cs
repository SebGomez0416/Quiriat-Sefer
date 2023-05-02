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
    

    [Header("Settings")]
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

    private void Update()
    {
        //_turretState.UpdateState();
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
        turretmat.materials[1].color = state ? standart : colorless;
        barrelmat.materials[1].color = state ? standart : colorless;
        
        //activa y desactiva la barra de vida
        _ui.SetActive(state);
    }

    private void UpdateLifeBar()
    {
        _ui.transform.forward = iaSensor.Target.position-_ui.transform.position;
        lifeBar.fillAmount = life/maxLife;
    }
}
