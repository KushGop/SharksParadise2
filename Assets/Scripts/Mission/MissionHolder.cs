using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissionHolder : MonoBehaviour
{
  public GameObject mission;
  public MissionStats missionStats;
  private GameObject missionObject;

  private void Start()
  {
    for (int i = 0; i < 3; i++)
    {
      missionObject = Instantiate(mission, transform);
      //missionObject.GetComponent<Mission>().SetMissionString(missionStats.missions.ElementAt(i).Value.GetMissionString());
    }
  }

}
