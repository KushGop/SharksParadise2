using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightMusic : MonoBehaviour
{
  [SerializeField] AudioSource music;
  private void Start()
  {
    GameManager.switchToDay += PlayMusic;
    GameManager.pause += music.Pause;
    GameManager.unpause += music.UnPause;
  }
  private void OnDestroy()
  {
    GameManager.switchToDay -= PlayMusic;
    GameManager.pause -= music.Pause;
    GameManager.unpause -= music.UnPause;
  }
  private void PlayMusic()
  {
    music.Play();
  }
}
