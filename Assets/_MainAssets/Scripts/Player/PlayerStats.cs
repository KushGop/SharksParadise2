using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Sharks Paradise/PlayerStats", order = 0)]
public class PlayerStats : ScriptableObject
{
  public Vector3 playerPosition;
  public float playerSize;
  public float r,s;
  public float energy;
  public Transform playerScript;
  public int points;
}
