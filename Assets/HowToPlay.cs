using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HowToPlay : MonoBehaviour
{
  int slideNum;
  [SerializeField] TextMeshProUGUI navValue;
  [SerializeField] Transform slides;
  [SerializeField] Button left;
  [SerializeField] Button right;
  [SerializeField] Button okay;

  private void OnEnable()
  {
    slideNum = 0;
    navValue.text = "1/" + slides.childCount;
    for (int i = 1; i < slides.childCount; i++)
      slides.GetChild(i).gameObject.SetActive(false);

    slides.GetChild(0).gameObject.SetActive(true);
    left.interactable = false;
    right.interactable = true;
    okay.gameObject.SetActive(GameManager.hasSeenTutorial);
  }

  public void NavLeft()
  {
    slides.GetChild(slideNum).gameObject.SetActive(false);
    slides.GetChild(--slideNum).gameObject.SetActive(true);
    navValue.text = (slideNum + 1) + "/" + slides.childCount;
    right.interactable = true;
    if (slideNum == 0)
    {
      left.interactable = false;
      slideNum = 0;
    }
  }
  public void NavRight()
  {
    slides.GetChild(slideNum).gameObject.SetActive(false);
    slides.GetChild(++slideNum).gameObject.SetActive(true);
    navValue.text = (slideNum + 1) + "/" + slides.childCount;
    left.interactable = true;
    if (slideNum + 1 >= slides.childCount)
    {
      okay.gameObject.SetActive(true);
      right.interactable = false;
      slideNum = slides.childCount - 1;

    }
  }
}
