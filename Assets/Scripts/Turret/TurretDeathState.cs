using System;
using UnityEngine;

public class TurretDeathState : ICharacterStates
{
    private Turret _turret;
    private bool death;
    private float timeToExplosion;
    
    public static event Action OnDying;

    public TurretDeathState(Turret turret)
    {
        _turret = turret;
    }

    public Type UpdateState()
    {
        timeToExplosion += Time.deltaTime;
        ActiveExplosion();
        
        _turret.NewState = null;
        Transform.Destroy(_turret.gameObject,4f);

        return _turret.NewState;
    }
    
    private void ActiveExplosion()
    {
        if (!death)
        {    
            death = true;
            _turret.Explosion.Play();
        }

        if (!(timeToExplosion >= 1f)) return;
        timeToExplosion = -10f;
        OnDying?.Invoke();
        _turret.TurretMat.enabled = false;
        _turret.BarrelMat.enabled = false;
        _turret.UI.SetActive(false);
    }

}
