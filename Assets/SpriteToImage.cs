using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteToImage : MonoBehaviour
{
  [SerializeField] SpriteRenderer sprite;
  [SerializeField] Image image;

  private void LateUpdate()
  {
    image.sprite = sprite.sprite;
  }
}
