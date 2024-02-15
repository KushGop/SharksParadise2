using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreMission : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI skipOrClaimText;
  [SerializeField] private TextMeshProUGUI missionText;
  [SerializeField] private Image icon;
  [SerializeField] private MissionIcons_ScritableObject icons;
  protected Vector3 origin;
  private Mission mission;
  private int coins;

  [SerializeField] private float r;
  [SerializeField] private float s;
  private Vector3 pos = Vector3.zero;
  [SerializeField] private GameObject coinPrefab;

  private void Start()
  {
    origin = icon.transform.position;
    //Check if mission is complete
    if (mission.isComplete)
    {
      CompleteMissionAnimation();
    }
    //Add coins to total coins
    //Change Mission

    //CASE 1: Player waits for animation
    //Show animation
    //Update coin text, but not game manager
    //Have an On Hold mission list to easily switch out missions
    //When animation is complete, show new mission, dont update game manager

    //DEFAULT (CASE 2):
    //Update hold missions and missions on button click, Play again or main menu
    //update game manger coins, missions, hold missions
    //save locally on click


    //CASE 3: Player exits game
    //Update hold missions and missions on exit
    //update game manger coins, missions, hold missions
    //save locally on exit
    //
  }

  public void SetMission(Mission mission)
  {
    this.mission = mission;

    missionText.text = this.mission.text;
    icon.sprite = icons.iconDictionary[mission.missionName];
    //TODO: Add button
    //SetSkipOrClaimButton();
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
    int numCoins = mission.coins / 10;
    for (int i = 0; i < numCoins; i++)
    {
      Instantiate(coinPrefab, SetPosition(), SetRotation(), transform);
      //AddCoin();
    }


    //Coins trail towards coin count and increase its text
    //Use Colliders to increment text by 10
    //Play sound on collision
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

  private void AddCoin()
  {
  }


  private void SetSkipOrClaimButton()
  {
    if (mission.isComplete)
    {
      //claim coins
      coins = mission.coins;
      skipOrClaimText.text = "Claim " + coins.ToString();
      //update total coins
    }
    else
    {
      //skip with ad
      skipOrClaimText.text = "Skip";
    }
  }
}
