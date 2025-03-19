using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gley.GameServices;

public class Login : MonoBehaviour
{
  void Start()
  {
    TryLogIn();
  }
  public void TryLogIn()
  {
    if (!API.IsLoggedIn())
      API.LogIn();
  }
}
