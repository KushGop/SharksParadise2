using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
  public Animator bubbles;

  public void ChangeSceneTo(string name)
  {
    StartCoroutine(PlayBubbles(name));
  }

  private IEnumerator PlayBubbles(string name){
    bubbles.Play("Bubbles");
    yield return new WaitForSeconds(1f);
    
    SceneManager.LoadScene(name);
  }

}
