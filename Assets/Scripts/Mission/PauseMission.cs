using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PauseMission : MonoBehaviour
{

  [SerializeField] private TextMeshProUGUI missionText;
  [SerializeField] private TextMeshProUGUI coinAmount;
  [SerializeField] private TextMeshProUGUI progress;
  [SerializeField] private Image icon;
  [SerializeField] private MissionIcons_ScritableObject icons;
  protected Vector3 origin;
  private Mission mission;
  private int count;
  private bool isSet = false;

  public void SetMission(Mission mission)
  {
    this.mission = mission;
    missionText.text = this.mission.text;
    icon.sprite = icons.iconDictionary[mission.missionName];
    coinAmount.text = mission.coins.ToString();
    count = mission.count;
    isSet = true;
    UpdateProgress();
  }

  private void OnEnable()
  {
    if (isSet)
      UpdateProgress();
  }

  public void UpdateProgress()
  {
    if (MissionData.missionDictionary[mission.missionName] >= count)
      progress.text = count + "/" + count;
    else
      progress.text = MissionData.missionDictionary[mission.missionName] + "/" + count;
  }


}
