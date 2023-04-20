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
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
                _player.Agent.destination = hit.point;

            _player.isDoubleClick();
        }

        return _player.Agent.velocity != Vector3.zero ? _player.NewState : null;
    }

    
}
