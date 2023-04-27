using System;
using UnityEngine;

public class PlayerRunState : ICharacterStates
{
    private Player _player;

    public PlayerRunState(Player player)
    {
        _player = player;
    }
    
    public Type UpdateState()
    { 
        _player.Animator.SetTrigger("Run");
        SetWeapon();

        _player.IsDoubleClick();
        _player.IsShoot();
        if (_player.IsDamage) _player.NewState = typeof(PlayerDeathState);

        return _player.Agent.velocity == Vector3.zero && !_player._isShoot  &&!_player.IsDamage? typeof(PlayerIdleState) : _player.NewState;
    }
    
    private void SetWeapon()
    {
        _player.Weapon.SetActive(false);
        _player.WeaponBack.SetActive(true);
    }
}
