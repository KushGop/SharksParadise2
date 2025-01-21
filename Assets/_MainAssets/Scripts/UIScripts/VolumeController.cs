using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
  public AudioSource music;
  public AudioSource[] sfx;
  public Slider masterSlider,musicSlider,sfxSlider;

  private void Start() {
    AdjustMasterVolume(masterSlider.value);
    AdjustMusicVolume(musicSlider.value);
    AdjustSfxVolume(sfxSlider.value);
  }

  public void AdjustMasterVolume(float value)
  {
    AudioListener.volume = value;
  }
  public void AdjustMusicVolume(float value)
  {
    music.volume = value;
  }
  public void AdjustSfxVolume(float value)
  {
    foreach (AudioSource s in sfx)
    {
      s.volume = value;
    }
  }
}
