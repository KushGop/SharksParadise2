using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{

  [SerializeField] private TextMeshProUGUI count;
  [SerializeField] private ChangeScene changeScene;
  [SerializeField] private ExitGame exitGame;
  [SerializeField] private PolygonCollider2D playercollider;
  [SerializeField] private PlayerMovement playerMovement;
  private int startTime;

  private void Start()
  {
    startTime = 5;
  }

  private void OnEnable()
  {
    count.text = "5";
    StartCoroutine(countDown());
  }

  IEnumerator countDown()
  {
    yield return new WaitForSecondsRealtime(1f);
    startTime--;
    count.text = startTime.ToString();
    if (startTime > 0)
    {
      StartCoroutine(countDown());
    }
    else
    {
      GiveUp();
    }

  }
    public void Revive()
    {
        Time.timeScale = 0;
        Gley.MobileAds.API.ShowRewardedVideo(CompleteMethod);
    }

    private void CompleteMethod(bool completed)
    {
        if (completed)
        {
            ContinueGame();
           // coins += 100;
        }
    }
    public void ContinueGame()
  {
    StopAllCoroutines();
    transform.parent.gameObject.SetActive(false);
    playercollider.enabled = true;
    playerMovement.TriggerPower(0);
    Time.timeScale = 1;
  }

  public void GiveUp()
  {
    Gley.MobileAds.API.ShowInterstitial();
    exitGame.ExitGameSequence();
    changeScene.ChangeSceneTo("Score");
  }
}
