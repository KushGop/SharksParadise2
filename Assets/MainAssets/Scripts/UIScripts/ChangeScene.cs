using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Gley.MobileAds;

public class ChangeScene : MonoBehaviour
{
  public Animator bubbles;
  public AudioSource sound;

  public void ChangeSceneTo(string name)
  {
    bubbles.gameObject.SetActive(true);
    //if (name.Equals("Score"))
    //{
    //  //show interstitial ad
    //  Time.timeScale = 0;
    //  //API.ShowInterstitial();
    //}
    StartCoroutine(PlayBubbles(name));
  }

  private IEnumerator PlayBubbles(string name)
  {
    Time.timeScale = 1;
    yield return new WaitForSeconds(0.1f);
    sound.Play();
    bubbles.Play("Bubbles");
    yield return new WaitForSeconds(0.5f);

    SceneManager.LoadScene(name);
  }

}
