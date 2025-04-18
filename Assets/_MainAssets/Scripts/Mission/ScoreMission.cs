using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreMission : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI missionText;
  [SerializeField] private TextMeshProUGUI coinAmount;
  [SerializeField] private Image icon;
  [SerializeField] private CanvasGroup canvasGroup;
  [SerializeField] private MissionIcons_ScritableObject icons;
  [SerializeField] private Transform coinOrigin;
  protected Vector3 origin;
  private Mission mission;
  private int index;
  private int coins;
  private bool isComplete;

  [SerializeField] private float r;
  [SerializeField] private float s;
  private Vector3 pos = Vector3.zero;
  [SerializeField] private GameObject coinPrefab;
  [SerializeField] private GameObject adButton;

  private void Start()
  {
    origin = coinOrigin.position;
    //Check if mission is complete
    if (isComplete)
    {
      CompleteMissionAnimation();
    }
  }

  public void SetMission(Mission mission)
  {
    this.mission = mission;
    isComplete = mission.isComplete;
    missionText.text = this.mission.text;
    icon.sprite = icons.iconDictionary[mission.missionName];
    coinAmount.text = mission.coins.ToString();
    coins = mission.coins;
    index = mission.indexInManager;
    //TODO: Add button
    //SetSkipOrClaimButton();
  }

  public int getMissionIndex()
  {
    return index;
  }

  public void SkipMission()
  {
    //update game manager, change is complete to true
    //Go through CASE 1 when ad is finished
  }

  private void CompleteMissionAnimation()
  {
    //Scratch out mission and play sound
    //Spawn 10,20,30 coins based on its value
    adButton.SetActive(false);
    int numCoins = coins / 10;
    GameManager.missionCoins = coins;
    for (int i = 0; i < numCoins; i++)
    {
      Instantiate(coinPrefab, SetPosition(), SetRotation(), transform);
      //AddCoin();
    }

    StartCoroutine(UpdateNewMission());
    //Coins trail towards coin count and increase its text
    //Use Colliders to increment text by 10
    //Play sound on collision
  }

  private IEnumerator UpdateNewMission()
  {
    yield return new WaitForSecondsRealtime(2f);
    StartCoroutine(SwitchMission());
  }

  IEnumerator SwitchMission()
  {
    // loop over 1 second backwards
    for (float i = 1; i >= 0; i -= Time.deltaTime)
    {
      // set color with i as alpha
      canvasGroup.alpha = i;
      yield return null;
    }
    // fade from transparent to opaque
    SetMission(mission);
    adButton.SetActive(true);
    // loop over 1 second
    for (float i = 0; i <= 1; i += Time.deltaTime)
    {
      // set color with i as alpha
      canvasGroup.alpha = i;
      yield return null;
    }
  }

  protected virtual Vector3 SetPosition()
  {
    float a = Random.Range(0f, 1f);
    a = a * 2 * Mathf.PI;
    float b = Random.Range(r, s);
    pos.x = b * Mathf.Cos(a) + origin.x;
    pos.y = b * Mathf.Sin(a) + origin.y;
    return pos;
  }
  protected virtual Quaternion SetRotation()
  {
    return Quaternion.Euler(0, 0, Random.Range(0, 360));
  }
}
