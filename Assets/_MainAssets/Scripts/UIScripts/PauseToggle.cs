using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseToggle : MonoBehaviour
{
  [SerializeField] private GameObject optionsMenu, icon;
  private void Start()
  {
    optionsMenu.SetActive(false);
    GameManager.pause += PauseGame;
    GameManager.unpause += ResumeGame;
  }
  private void OnDestroy()
  {
    GameManager.pause -= PauseGame;
    GameManager.unpause -= ResumeGame;
  }
  public void Toggle()
  {
    if (optionsMenu.activeSelf)
    {
      optionsMenu.SetActive(false);
      icon.SetActive(true);
      GameManager.unpause();
    }
    else
    {
      optionsMenu.SetActive(true);
      icon.SetActive(false);
      GameManager.pause();
    }
  }

  private void PauseGame()
  {
    //Gley.MobileAds.API.ShowInterstitial();
    Time.timeScale = 0;
  }
  private void ResumeGame()
  {
    Time.timeScale = 1;
    DataPersistenceManager.instance.SaveGame();
  }
}
