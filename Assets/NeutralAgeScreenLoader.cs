using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Gley.GameServices;

public class NeutralAgeScreenLoader : MonoBehaviour, IDataPersistence
{
  [SerializeField] GameObject canvas;
  [SerializeField] Slider slider;
  [SerializeField] NeutralAgeScreen screen;
  public void LoadData(GameData data)
  {
    if (data.age != 0)
    {
      API.LogIn();
      screen.SetAge(data.age);
      SceneManager.LoadScene("MainMenu");
    }
    else
    {
      canvas.SetActive(true);
    }
  }

  public void SaveData(GameData data)
  {
    data.age = ((int)slider.value);
    SceneManager.LoadScene("MainMenu");
  }
}
