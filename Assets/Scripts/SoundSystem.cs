using System;
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
      Pause .OnPause += OnPause;
      BotDeathState.OnExplosion +=Explosion;
      ColossusDeathState.OnExplosion +=Explosion;
      TurretDeathState.OnExplosion +=Explosion;
      ClickGround.OnClickGround += Click;
      UISettings.OnMuteMusic += MuteMusic;
      UISettings.OnMuteSfx += MuteSfx;
      Pause.OnChangeScene += ChangeScene;
      Menu.OnChangeScene += ChangeScene;
   }

   private void OnDisable()
   {
      Pause .OnPause -= OnPause;
      BotDeathState.OnExplosion -=Explosion;
      ColossusDeathState.OnExplosion -=Explosion;
      ClickGround.OnClickGround -= Click;
      TurretDeathState.OnExplosion -=Explosion;
      UISettings.OnMuteMusic -= MuteMusic;
      UISettings.OnMuteSfx -= MuteSfx;
      Pause.OnChangeScene -= ChangeScene;
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
   private void Explosion()
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
