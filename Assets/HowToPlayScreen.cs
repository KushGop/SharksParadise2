using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayScreen : MonoBehaviour
{

  [SerializeField] AudioSource music;

  void Start()
  {
    if (GameManager.hasSeenTutorial)
    {
      gameObject.SetActive(false);
      music.Play();
    }
    else
    {
      //pause time
      //dont show got it button
      Time.timeScale = 0;
      music.Pause();
    }
  }

  public void FinishedTutorial()
  {
    if (!GameManager.hasSeenTutorial)
    {
      GameManager.hasSeenTutorial = true;
      Time.timeScale = 1;
      music.Play();
      DataPersistenceManager.instance.SaveGame();
    }
  }
}
