using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseToggle : MonoBehaviour
{
  [SerializeField] private GameObject optionsMenu, icon;
  private void Start()
  {
    optionsMenu.SetActive(false);
  }
  public void toggle()
  {
    if (optionsMenu.activeSelf)
    {
      optionsMenu.SetActive(false);
      icon.SetActive(true);
      resumeGame();
      GameManager.unpause();
    }
    else
    {
      optionsMenu.SetActive(true);
      icon.SetActive(false);
      pauseGame();
      GameManager.pause();
    }
  }

  private void pauseGame()
  {
    //Gley.MobileAds.API.ShowInterstitial();
    Time.timeScale = 0;
  }
  private void resumeGame()
  {
    Time.timeScale = 1;
    DataPersistenceManager.instance.SaveGame();
  }
}
