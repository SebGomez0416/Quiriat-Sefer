using UnityEngine;

public class SoundSystem : MonoBehaviour
{
   [SerializeField] private AudioSource shoot;
   [SerializeField] private AudioSource explosion;
   [SerializeField] private AudioSource click;
   [SerializeField] private AudioSource music;


   private void OnEnable()
   {
      Pause .OnPause += OnPause;
      BotDeathState.OnExplosion +=Explosion;
      ColossusDeathState.OnExplosion +=Explosion;
      TurretDeathState.OnExplosion +=Explosion;
      ClickGround.OnClickGround += Click;
   }

   private void OnDisable()
   {
      Pause .OnPause -= OnPause;
      BotDeathState.OnExplosion -=Explosion;
      ColossusDeathState.OnExplosion -=Explosion;
      ClickGround.OnClickGround -= Click;
      TurretDeathState.OnExplosion +=Explosion;
   }

   private void OnPause()
   {
      if (DataBetweenScenes.instance.isPaused)
      {
         music.Pause();
      }
      else  music.Play();
      
   }

   private void Explosion()
   {
      explosion.Play();
   }

   private void Click()
   {
      click.Play();
   }
   
}
