using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TipScript : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI tip;

  private int index;
  private string[] tips =
  {"Tip! the back of your shark turns red when a big shark is near by!",
    "Watch out for squids! They spray ink when scared!",
    "Jellyfish tentacles can sting you! Their heads are safe!",
    "Want diamonds? Find treasure chests out in the ocean!",
    "Gold fish will leave a trail of coins when startled!",
    "Tired of swimming? Try jumping over bigger sharks!",
  };

  private void Start()
  {
    index = Random.Range(0, tips.Length);
    tip.text = tips[index];
    StartCoroutine(SwitchTip());
  }

  IEnumerator SwitchTip()
  {
    yield return new WaitForSeconds(4);
    for (float i = 1; i >= 0; i -= Time.deltaTime)
    {
      tip.color = new Color(255, 255, 255, i);
      yield return null;
    }
    index++;
    tip.text = tips[index % tips.Length];
    for (float i = 0; i <= 1; i += Time.deltaTime)
    {
      tip.color = new Color(255, 255, 255, i);
      yield return null;
    }
    StartCoroutine(SwitchTip());
  }
}
