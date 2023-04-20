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

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
                _player.Agent.destination = hit.point;
            _player.isDoubleClick();
        }

        return _player.Agent.velocity == Vector3.zero ? typeof(PlayerIdleState) : _player.NewState;
    }
    
    private void SetWeapon()
    {
        _player.Weapon.SetActive(false);
        _player.WeaponBack.SetActive(true);
    }
}
