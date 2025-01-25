using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Gley.EasyIAP;

public class SnapToItem : MonoBehaviour
{
  [SerializeField] ScrollRect scrollRect;
  [SerializeField] RectTransform contentPanel;
  [SerializeField] RectTransform listItem;

  [SerializeField] HorizontalLayoutGroup HLG;
  [SerializeField] TMP_Text label;
  [SerializeField] string[] itemNames;
  [SerializeField] float snapForce;
  [SerializeField] float buff;

  [SerializeField] Closet_ScritableObject closet;
  [SerializeField] GameObject hatPrefab;
  [SerializeField] Transform closetTransform;

  [SerializeField] HatLoader loader;

  bool isSnapped;
  float snapSpeed;
  int currentItem;
  int currentIndex;
  int currentItemBuff = 5;

  HatScript hs;

  private void Start()
  {

    isSnapped = false;
  }

  public void SetCurrentItem(int index)
  {
    currentIndex = index;
    currentItem = index + currentItemBuff;
    contentPanel.localPosition = new Vector3(
        0 - (currentItem * (listItem.rect.width + HLG.spacing + buff)),
        contentPanel.localPosition.y,
        contentPanel.localPosition.z);
  }
  public void UpdateHatScript()
  {

  }


  private void LateUpdate()
  {
    if (scrollRect.velocity.magnitude < 200 && !isSnapped)
    {
      scrollRect.velocity = Vector2.zero;
      snapSpeed += snapForce * Time.deltaTime;
      contentPanel.localPosition = new Vector3(
        Mathf.MoveTowards(contentPanel.localPosition.x, 0 - (currentItem * (listItem.rect.width + HLG.spacing + buff)), snapSpeed),
        contentPanel.localPosition.y,
        contentPanel.localPosition.z);
      if (contentPanel.localPosition.x == 0 - (currentItem * (listItem.rect.width + HLG.spacing + buff)))
      {
        isSnapped = true;
      }
    }
    else
    {
      isSnapped = false;
      snapSpeed = 0;
    }
    currentItem = Mathf.RoundToInt((0 - contentPanel.localPosition.x / (listItem.rect.width + HLG.spacing + buff)));
    currentIndex = currentItem - currentItemBuff;
    hs = closetTransform.GetChild(currentIndex).GetComponent<HatScript>();
    print(hs.hatName + " isUnlocked: " + hs.isUnlocked);
    if (hs.isUnlocked)
    {
      if (hs.isSelected)
      {
        label.text = "Selected";
      }
      else
      {
        label.text = "select";
      }
    }
    else
    {
      label.text = hs.price;
    }
  }

  public void ButtonClick()
  {
    if (hs.isUnlocked)
    {
      HatManager.ClearChoice();
      closetTransform.GetChild(currentIndex).GetComponent<HatScript>().isSelected = true;
      loader.SetHat(hs.hatName);
      DataPersistenceManager.instance.SaveGame();
    }
    else
    {
      BuyHat();
    }
  }

  public void BuyHat()
  {
    API.BuyProduct(closetTransform.GetChild(currentIndex).GetComponent<HatScript>().hatName, ProductBought);
  }

  private void ProductBought(IAPOperationStatus status, string message, StoreProduct product)
  {
    if (status == IAPOperationStatus.Success)
      closetTransform.GetChild(currentIndex).GetComponent<HatScript>().isUnlocked = true;
    else if (status == IAPOperationStatus.Fail)
      print("Purchase Failed");
  }

}
