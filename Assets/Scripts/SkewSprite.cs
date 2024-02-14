using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkewSprite : MonoBehaviour
{

  [SerializeField] SpriteRenderer spriteRenderer;

  void Start()
  {
    Sprite sprite = spriteRenderer.sprite;
  }
}
