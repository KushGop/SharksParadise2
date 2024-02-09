
using UnityEngine;

public class MissionHolder : MonoBehaviour
{
  public GameObject mission;

  private void Start()
  {
    for (int i = 0; i < 3; i++)
    {
      Instantiate(mission, transform);
    }
  }

}
