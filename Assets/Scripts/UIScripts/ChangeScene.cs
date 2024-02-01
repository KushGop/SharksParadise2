using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Gley.MobileAds;

public class ChangeScene : MonoBehaviour
{
  public Animator bubbles;

  public void ChangeSceneTo(string name)
  {
    if (name.Equals("MainMenu") || name.Equals("Score"))
    {
      //show interstitial ad
      Time.timeScale = 0;
      API.ShowInterstitial();
    }
    StartCoroutine(PlayBubbles(name));
  }

  private IEnumerator PlayBubbles(string name)
  {
    yield return new WaitForSeconds(0.1f);
    bubbles.Play("Bubbles");
    yield return new WaitForSeconds(1f);

    SceneManager.LoadScene(name);
  }

}
