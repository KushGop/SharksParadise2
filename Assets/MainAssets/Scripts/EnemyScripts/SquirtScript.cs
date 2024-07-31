using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirtScript : MonoBehaviour
{

  public AudioSource sound;

  // Start is called before the first frame update
  public void playSquirt()
  {
    sound.Play();
  }
}
