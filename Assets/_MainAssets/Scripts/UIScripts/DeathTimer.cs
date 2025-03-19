using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Gley.MobileAds;

public class DeathTimer : MonoBehaviour
{

  [SerializeField] private TextMeshProUGUI count;
  //[SerializeField] private TextMeshProUGUI availableCoins;
  [SerializeField] private ChangeScene changeScene;
  [SerializeField] private ExitGame exitGame;
  [SerializeField] private PolygonCollider2D playercollider;
  [SerializeField] private SpriteRenderer playerSprite;
  [SerializeField] private GameObject colors;
  [SerializeField] private GameObject shock;
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
    startTime = countdownTime;
    GameManager.TurnCoinsOn();
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
    if (API.IsRewardedVideoAvailable())
    {
      StopAllCoroutines();
      Time.timeScale = 0;
      API.ShowRewardedVideo(CompleteMethod);
    }
  }

  private void CompleteMethod(bool completed)
  {
    if (completed)
    {
      ContinueGame();
    }
    else
    {
      Time.timeScale = 1;
      StartCoroutine(CountDown());
    }
  }
  public void ContinueGame()
  {
    GameManager.isAlive = true;
    playerMovement.SetIsInvincible(true);
    StopAllCoroutines();
    playerMovement.TriggerPower(-1);

    transform.parent.gameObject.SetActive(false);
    playercollider.enabled = true;

    //Time.timeScale = 0.25f;
    GameManager.TurnCoinsOff();
    GameManager.unpause();
  }

  public void GiveUp()
  {
    Gley.GameServices.API.SubmitScore(Mathf.Max(GameManager.highscore, GameManager.score), Gley.GameServices.LeaderboardNames.Highscores);
    DataPersistenceManager.instance.SaveGame();
    playerSprite.color = new Color(0, 0, 0, 0);
    colors.SetActive(false);
    shock.SetActive(false);
    if (API.CanShowAds())
    {
      if (GameManager.adCount == 2)
      {
        GameManager.adCount = 0;
        API.ShowInterstitial(() =>
                  {
                    ExitGame();
                  });
      }
      else
      {
        GameManager.adCount++;
        ExitGame();
      }

    }
    else
    {
      ExitGame();
    }

    void ExitGame()
    {
      exitGame.ExitGameSequence();
      changeScene.ChangeSceneTo("Score");
    }
  }


}
