using System;
using UnityEngine;

public class PlayerDeathState : ICharacterStates
{
    private Player _player;
    private float time;

    public PlayerDeathState(Player player)
    {
        _player = player;
    }

    public Type UpdateState()
    {
        _player.Agent.isStopped = true;
        _player.Agent.destination = _player.transform.position;
        Timer();
        
        if (_player.Life > 0 && _player.IsDamage)
        {
            _player.Animator.SetTrigger("Hurt");
            SetWeapon();
        }
        else if(_player.Life <= 0 && _player.IsDamage)
        {
            _player.Animator.SetTrigger("Death");
            _player.Weapon.SetActive(false);
            _player.WeaponBack.SetActive(false);
            _player.NewState = null;
            Transform.Destroy(_player.gameObject,5f);
        }

        if (!_player.IsDamage)
        {
            _player.IsDoubleClick();
            _player.IsShoot();
            if (_player.Agent.velocity == Vector3.zero && !_player._isShoot) _player.NewState=typeof(PlayerIdleState);
        }
        return _player.NewState;
    }
    
    private void Timer()
    {
        time += Time.deltaTime;
        if (!(time >= 0.5f)) return;
        _player.IsDamage = false;
        time = 0;
    }
    
    private void SetWeapon()
    {
        _player.Weapon.SetActive(true);
        _player.WeaponBack.SetActive(false);
    }
}
