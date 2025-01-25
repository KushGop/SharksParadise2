using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gley.EasyIAP;

public class HatScript : MonoBehaviour
{
  internal string price;
  internal bool isUnlocked;
  internal bool isSelected;
  internal int index;
  internal ShopProductNames hatName;
  [SerializeField] Button button;
  [SerializeField] Colors_SO colors;
  [SerializeField] Image background;
  [SerializeField] GameObject buy;

  private void Start()
  {
    HatManager.ClearChoice += SetColor;
    HatManager.ClearSelection += () => isSelected = false;
  }

  public void SetValues(ShopProductNames name, string price, bool isUnlocked, bool isSelected, ClosetManager closetManager)
  {
    hatName = name;
    this.price = price;
    this.isUnlocked = isUnlocked;
    this.isSelected = isSelected;
    button.onClick.AddListener(() => closetManager.SelectHat(this));
    SetColor();
    if (isSelected)
    {
      HighlightChoice();
    }
    buy.SetActive(!isUnlocked);
  }

  private void SetColor()
  {
    background.color = isSelected ? colors.colors[1] : colors.colors[0];
  }

  public void HighlightChoice()
  {
    background.color = (isSelected ? colors.colors[1] : colors.colors[0]) * Color.grey;
  }
  public void OnChoice()
  {
    //unhighlight everything
    HatManager.ClearChoice();
    //highlight self
    HighlightChoice();
  }
  public void Purchased()
  {
    buy.SetActive(false);
  }
}
