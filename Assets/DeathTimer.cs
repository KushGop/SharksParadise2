using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{

  [SerializeField] private TextMeshProUGUI count;
  [SerializeField] private ChangeScene changeScene;
  [SerializeField] private ExitGame exitGame;
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
      exitGame.ExitGameSequence();
      changeScene.ChangeSceneTo("Score");
    }

  }
}
