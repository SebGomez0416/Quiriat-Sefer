using System;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
  [SerializeField] private Sprite music;
  [SerializeField] private Sprite sfx;
  [SerializeField] private Sprite muteSfx;
  [SerializeField] private Sprite muteMusic;

  [SerializeField] private Button buttonMusic;
  [SerializeField] private Button buttonSfx;

  public static event Action OnMuteMusic;
  public static event Action OnMuteSfx;

  private void Start()
  {
    buttonMusic.image.sprite = DataBetweenScenes.instance.muteMusic ? muteMusic : music;
    buttonSfx.image.sprite = DataBetweenScenes.instance.muteSFX ? muteSfx : sfx;
  }

  public void MuteMusic()
  {
    DataBetweenScenes.instance.muteMusic = !DataBetweenScenes.instance.muteMusic;
    buttonMusic.image.sprite = DataBetweenScenes.instance.muteMusic ? muteMusic : music;
    OnMuteMusic?.Invoke();
  }

  public void MuteSfx()
  {
    DataBetweenScenes.instance.muteSFX = !DataBetweenScenes.instance.muteSFX;
    buttonSfx.image.sprite = DataBetweenScenes.instance.muteSFX ? muteSfx : sfx;
    OnMuteSfx?.Invoke();
  }
}
