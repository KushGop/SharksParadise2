using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{

  [SerializeField] Sprite soundOn;
  [SerializeField] Sprite soundOff;
  [SerializeField] Image soundIcon;

  private void Start()
  {
    AudioListener.volume = GameManager.isMusicOn ? 1 : 0;
    soundIcon.sprite = GameManager.isMusicOn ? soundOn : soundOff;
  }

  public void ToggleSound()
  {
    GameManager.isMusicOn = !GameManager.isMusicOn;
    AudioListener.volume = GameManager.isMusicOn ? 1 : 0;
    soundIcon.sprite = GameManager.isMusicOn ? soundOn : soundOff;
    DataPersistenceManager.instance.SaveGame();
  }
}
