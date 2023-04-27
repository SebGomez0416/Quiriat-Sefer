using System;
using UnityEditor;
using UnityEngine;

public class PlayerShootState : ICharacterStates
{
    private Player _player;

    public PlayerShootState(Player player)
    {
        _player = player;
    }
    
    public Type UpdateState()
    { 
        _player.Animator.SetTrigger("Shoot");
        SetWeapon();

        if (_player.EnemyPosition != null)
            _player.transform.forward = _player.EnemyPosition.transform.position - _player.transform.position;


        if (_player._isShoot)
        { 
            if (Time.time > _player.ShotRateTime)
            {
               
                GameObject newBullet;
                newBullet = Editor.Instantiate(_player.Bullet, _player.SpawnPoint.position, _player.SpawnPoint.rotation);
                newBullet.GetComponent<Rigidbody>().AddForce(_player.SpawnPoint.forward*_player.ShotForce);
                _player.ShotRateTime = Time.time + _player.ShotRate;
                Editor.Destroy(newBullet,4);
            }
           
        }
        _player.IsDoubleClick(); 
        _player.IsShoot();
        if (_player.IsDamage) _player.NewState = typeof(PlayerDeathState);

        return _player.Agent.velocity == Vector3.zero && !_player._isShoot &&!_player.IsDamage ? typeof(PlayerIdleState) : _player.NewState;
    }
    
    private void SetWeapon()
    {
        _player.Weapon.SetActive(true);
        _player.WeaponBack.SetActive(false);
    }
}
