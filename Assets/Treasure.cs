using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
  [SerializeField] private Sprite openChest;
  [SerializeField] private SpriteRenderer chest;

  [SerializeField] private Collider2D collider2d;
  private Color newColor = new Color32(255, 255, 255, 0);
  public bool isCollected;


  public void Collected()
  {
    isCollected = true;
    chest.sprite = openChest;
    collider2d.enabled = false;
    StartCoroutine(FadeAway());
  }

  IEnumerator FadeAway()
  {
    yield return new WaitForSeconds(2);
    float waitTime = 3f;
    while (chest.color.a > 0.01)
    {
      chest.color = Color.Lerp(chest.color, newColor, Time.deltaTime * waitTime);
      yield return null;
    }
    transform.parent.GetComponent<AbstractFactory>().SpawnObject(transform);
  }

}
