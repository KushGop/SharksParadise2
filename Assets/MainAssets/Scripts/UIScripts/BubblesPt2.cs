using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblesPt2 : MonoBehaviour
{
  [SerializeField] Animator bubbles;
  [SerializeField] AudioSource sound;
  void Start()
  {
    StartCoroutine(PlayBubbles());
  }

  IEnumerator PlayBubbles()
  {
    yield return null;
    sound.Play();
    bubbles.SetTrigger("PlayAnim");
    yield return new WaitForSeconds(0.5f);
    gameObject.SetActive(false);
  }

}
