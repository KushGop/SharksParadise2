using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
  [SerializeField] private Sprite openChest;
  [SerializeField] private Sprite closedChest;
  [SerializeField] private SpriteRenderer chest;
  [SerializeField] private AudioSource crateOpen;

  [SerializeField] private Collider2D collider2d;
  private Color newColor = new Color32(255, 255, 255, 0);
  public bool isCollected;

  private void OnEnable()
  {
    chest.color = Color.white;
    collider2d.enabled = true;
    chest.sprite = closedChest;
  }

  public void Collected()
  {
    isCollected = true;
    chest.sprite = openChest;
    collider2d.enabled = false;
    crateOpen.Play();
    StartCoroutine(FadeAway());
  }

  IEnumerator FadeAway()
  {
    yield return new WaitForSeconds(0.5f);
    float waitTime = 3f;
    while (chest.color.a > 0.01)
    {
      chest.color = Color.Lerp(chest.color, newColor, Time.deltaTime * waitTime);
      yield return null;
    }
    chest.color = newColor;
    transform.parent.GetComponent<AbstractFactory>().SpawnObject(transform);
  }

}
