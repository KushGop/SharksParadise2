using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
  [SerializeField] AudioSource music;

  private void Start()
  {
    GameManager.pauseBackground += music.Pause;
    GameManager.unpauseBackground += music.UnPause;
  }
  private void OnDestroy()
  {
    GameManager.pauseBackground -= music.Pause;
    GameManager.unpauseBackground -= music.UnPause;
  }
}
