using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
  [SerializeField] private Transform player;
  public PlayerStats stats;
  public PlayerMobileInput playerInput;
  public float smoothSpeed;
  public Vector3 offset, playerPos;
  public float offsetSmoothing;
  public int boostOffset;
  public float boostLerpTime;
  private float currentSize;
  private bool isBig;

  private void Start()
  {
    QualitySettings.vSyncCount = 0;

    currentSize = 10;
    transform.position = Vector3.zero;

    playerInput.OnBoostPressed += CameraBoostOn;
    playerInput.OnBoostReleased += CameraBoostOff;
  }

  //Lerps behind player
  void FixedUpdate()
  {
    playerPos = new Vector3(stats.playerPosition.x, stats.playerPosition.y, -10);
    transform.position = Vector3.Lerp(transform.position, playerPos, offsetSmoothing * Time.deltaTime);
  }

  // private void CameraSizeIncrease(int size)
  // {
  //   currentSize += (size / 10);
  //   Camera.main.orthographicSize = currentSize;
  // }

  //Camera zooms out on boost
  private void CameraBoostOn()
  {
    if (stats.energy != 0)
    {
      StartCoroutine(BoostHelper(currentSize, currentSize + boostOffset, boostLerpTime));
      isBig = true;
    }
  }

  //Zooms back in 
  private void CameraBoostOff()
  {
    if (isBig)
    {
      StartCoroutine(BoostHelper(currentSize + boostOffset, currentSize, boostLerpTime));
      isBig = false;
    }
  }

  //Helper to adjust camera size
  IEnumerator BoostHelper(float oldSize, float newSize, float time)
  {
    float elapsed = 0;
    while (elapsed <= time)
    {
      elapsed += Time.deltaTime;
      float t = Mathf.Clamp01(elapsed / time);

      Camera.main.orthographicSize = Mathf.Lerp(oldSize, newSize, t);
      yield return null;
    }

  }
}
