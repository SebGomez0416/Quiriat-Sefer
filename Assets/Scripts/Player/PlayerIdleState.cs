using System;
using UnityEngine;

public class PlayerIdleState : ICharacterStates
{
    private Player _player;

    public PlayerIdleState(Player player)
    {
        _player = player;
    }
    
    public Type UpdateState()
    {
        _player.Animator.SetTrigger("Idle");
        SetWeapon();

        _player.IsDoubleClick();
        _player.IsShoot();

        return _player.Agent.velocity != Vector3.zero || _player._isShoot ? _player.NewState : null;
    }

    private void SetWeapon()
    {
        _player.Weapon.SetActive(true);
        _player.WeaponBack.SetActive(false);
    }

    
}
