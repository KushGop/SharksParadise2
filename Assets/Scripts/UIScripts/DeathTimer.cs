using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Gley.MobileAds;

public class DeathTimer : MonoBehaviour
{

  [SerializeField] private TextMeshProUGUI count;
  [SerializeField] private TextMeshProUGUI availableCoins;
  [SerializeField] private ChangeScene changeScene;
  [SerializeField] private ExitGame exitGame;
  [SerializeField] private PolygonCollider2D playercollider;
  [SerializeField] private SpriteRenderer playerSprite;
  [SerializeField] private SpriteRenderer playerColor;
  [SerializeField] private PlayerMovement playerMovement;
  [SerializeField] private int countdownTime = 3;
  private int startTime;

  private void Start()
  {
    startTime = countdownTime;
  }

  private void OnEnable()
  {
    count.text = countdownTime.ToString();
    availableCoins.text = (GameManager.coins + GameManager.totalCoins).ToString();
    startTime = countdownTime;
    StartCoroutine(CountDown());
  }

  IEnumerator CountDown()
  {
    yield return new WaitForSecondsRealtime(1f);
    startTime--;
    count.text = startTime.ToString();
    if (startTime > 0)
    {
      StartCoroutine(CountDown());
    }
    else
    {
      GiveUp();
    }

  }
  public void ReviveAd()
  {
    StopAllCoroutines();
    Time.timeScale = 0;
    API.ShowRewardedVideo(CompleteMethod);
  }

  private void CompleteMethod(bool completed)
  {
    if (completed)
    {
      ContinueGame();
    }
  }
  public void ContinueGame()
  {
    playerMovement.SetIsInvincible(true);
    StopAllCoroutines();
    playerMovement.TriggerPower(0);



    availableCoins.text = GameManager.coins.ToString();
    transform.parent.gameObject.SetActive(false);
    playercollider.enabled = true;

    Time.timeScale = 1;
  }

  public void GiveUp()
  {
    playerSprite.color = new Color(0, 0, 0, 0);
    playerColor.color = new Color(0, 0, 0, 0);
    //API.ShowInterstitial();
    exitGame.ExitGameSequence();
    changeScene.ChangeSceneTo("Score");
  }
}
