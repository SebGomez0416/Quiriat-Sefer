using System;
using UnityEngine;

public class PlayerWalkState : ICharacterStates
{
    private Player _player;

    public PlayerWalkState(Player player)
    {
        _player = player;
    }
    
    public Type UpdateState()
    {
        _player.Animator.SetTrigger("Walk");
        SetWeapon();
       
       _player.IsDoubleClick();
       _player.IsShoot();

       return _player.Agent.velocity == Vector3.zero && !_player._isShoot ? typeof(PlayerIdleState) :_player.NewState;
    }
    
    private void SetWeapon()
    {
        _player.Weapon.SetActive(true);
        _player.WeaponBack.SetActive(false);
    }
}
