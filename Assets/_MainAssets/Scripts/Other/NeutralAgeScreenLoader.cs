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

  bool hasBeenSet;

  public void LoadData(GameData data)
  {
    print("Load Age");
    hasBeenSet = false;
    if (data.age != 0)
    {
      hasBeenSet = true;
      API.LogIn();
      screen.SetAge(data.age);
      SceneManager.LoadScene("LoadShop");
    }
    else
    {
      print("Loaded Age: " + data.age);
      canvas.SetActive(true);
    }
  }

  public void SaveData(GameData data)
  {
    if (!hasBeenSet)
    {
      print("Save age");
      data.age = ((int)slider.value);
      SceneManager.LoadScene("LoadShop");
    }
  }
}
