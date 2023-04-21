using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
   public ICharacterStates CurrentState { get; private set; }
   
   private Dictionary<Type, ICharacterStates> _playerStates;
   
   public PlayerStates(Player player)
   {
      _playerStates = new Dictionary<Type, ICharacterStates>()
      {
         { typeof(PlayerIdleState), new PlayerIdleState(player) },
         { typeof(PlayerRunState), new PlayerRunState(player) },
         { typeof(PlayerWalkState), new PlayerWalkState(player) },
         { typeof(PlayerShootState), new PlayerShootState(player) }
      };
      
      ChangeState(typeof(PlayerIdleState));
   }

   public void UpdateState()
   {
      Type newState = CurrentState.UpdateState();
      
      if (newState != null)
         ChangeState(newState);
   }
   
   private void ChangeState(Type newState)
   {
      CurrentState = _playerStates[newState];
   }
   
   
}
