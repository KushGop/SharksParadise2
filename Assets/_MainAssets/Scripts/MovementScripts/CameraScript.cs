using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
  [SerializeField] private Transform player;
  public PlayerStats stats;
  public MobileJoystick joystick;
  public float smoothSpeed;
  public Vector3 offset, playerPos;
  public float offsetSmoothing;
  public int boostOffset;
  public float boostLerpTime;
  private float currentSize;
  private void Start()
  {
    QualitySettings.vSyncCount = 0;

    currentSize = Camera.main.orthographicSize;
    transform.position = Vector3.zero;

    joystick.JumpPlayer += CameraZoomOut;
    //GameManager.fishEaten += CameraShake;
  }

  private void OnDisable()
  {
    joystick.JumpPlayer -= CameraZoomOut;
    //GameManager.fishEaten -= CameraShake;
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
  private void CameraZoomOut()
  {
    StartCoroutine(JumpHelper(currentSize, currentSize + boostOffset, boostLerpTime));
  }

  //Helper to adjust camera size
  IEnumerator JumpHelper(float oldSize, float newSize, float time)
  {
    float elapsed = 0;
    while (elapsed <= time)
    {
      elapsed += Time.deltaTime;
      float t = Mathf.Clamp01(elapsed / time);

      Camera.main.orthographicSize = Mathf.Lerp(oldSize, newSize, t);
      yield return null;
    }
    elapsed = 0;
    while (elapsed <= time)
    {
      elapsed += Time.deltaTime;
      float t = Mathf.Clamp01(elapsed / time);

      Camera.main.orthographicSize = Mathf.Lerp(newSize, oldSize, t);
      yield return null;
    }

  }
}
