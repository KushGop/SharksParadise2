using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunchSounds : MonoBehaviour
{
  public AudioClip[] munches;
  public AudioSource audioSource;

  public void playMunch(){
    audioSource.clip = munches[Random.Range(0,6)];
    audioSource.Play();
  }
}
