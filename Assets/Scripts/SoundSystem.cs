using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundSystem : MonoBehaviour
{
   [SerializeField] private AudioSource shoot;
   [SerializeField] private AudioSource explosion;
   [SerializeField] private AudioSource click;
   [SerializeField] private AudioSource music;
   

   [SerializeField] private AudioClip[] _audioClips;

   private void OnEnable()
   {
      UIController.OnPause += OnPause;
      BotDeathState.OnDying +=Dying;
      ColossusDeathState.OnDying +=Dying;
      TurretDeathState.OnDying +=Dying;
      ClickGround.OnClickGround += Click;
      UISettings.OnMuteMusic += MuteMusic;
      UISettings.OnMuteSfx += MuteSfx;
      UIController.OnChangeScene += ChangeScene;
      Menu.OnChangeScene += ChangeScene;
   }

   private void OnDisable()
   {
      UIController.OnPause -= OnPause;
      BotDeathState.OnDying -=Dying;
      ColossusDeathState.OnDying -=Dying;
      ClickGround.OnClickGround -= Click;
      TurretDeathState.OnDying -=Dying;
      UISettings.OnMuteMusic -= MuteMusic;
      UISettings.OnMuteSfx -= MuteSfx;
      UIController.OnChangeScene -= ChangeScene;
      Menu.OnChangeScene -= ChangeScene;
   }

   private void Start()
   {
      MuteMusic();
      MuteSfx();
   }

   private void OnPause()
   {
      if (DataBetweenScenes.instance.isPaused)
      {
         music.Pause();
      }
      else  music.Play();
      
   }
   private void Dying()
   {
      explosion.Play();
   }

   private void Click()
   {
      click.Play();
   }

   private void MuteMusic()
   {
      music.mute = DataBetweenScenes.instance.muteMusic;
   }
   
   private void MuteSfx()
   {
      shoot.mute = DataBetweenScenes.instance.muteSFX;
      explosion.mute = DataBetweenScenes.instance.muteSFX;
      click.mute = DataBetweenScenes.instance.muteSFX;
   }

   private void ChangeScene()
   {
      if (SceneManager.GetActiveScene().name == "GamePlay")
      {
         music.clip = _audioClips[1];
         music.Play();
      }
        
      if (SceneManager.GetActiveScene().name == "Menu")
      {
         music.clip = _audioClips[0];
         music.Play();
      }
        
   }
}
