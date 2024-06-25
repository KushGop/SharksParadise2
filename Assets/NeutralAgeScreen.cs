using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class NeutralAgeScreen : MonoBehaviour
{
  [SerializeField] Slider slider;
  [SerializeField] Button button;

  private void Start()
  {
    button.interactable = false;
  }

  public void SetButtonActive()
  {
    if (slider.value != 0)
      button.interactable = true;
    else
      button.interactable = false;
  }

  public void SetAge()
  {
    RequestConfiguration requestConfig = slider.value switch
    {
      >= 18 => new RequestConfiguration
      {
        MaxAdContentRating = MaxAdContentRating.PG,
        TagForChildDirectedTreatment = TagForChildDirectedTreatment.False
      },
      >= 12 => new RequestConfiguration
      {
        MaxAdContentRating = MaxAdContentRating.PG,
        TagForChildDirectedTreatment = TagForChildDirectedTreatment.True
      },
      >= 7 => new RequestConfiguration
      {
        MaxAdContentRating = MaxAdContentRating.PG,
        TagForChildDirectedTreatment = TagForChildDirectedTreatment.True
      },
      _ => new RequestConfiguration
      {
        MaxAdContentRating = MaxAdContentRating.G,
        TagForChildDirectedTreatment = TagForChildDirectedTreatment.True
      },
    };
    MobileAds.SetRequestConfiguration(requestConfig);
    if (MobileAds.GetRequestConfiguration() == null)
      print("null1");
    print("Rating: " + requestConfig.MaxAdContentRating.ToString());
    DataPersistenceManager.instance.SaveGame();
  }
  public void SetAge(int age)
  {
    RequestConfiguration requestConfig = age switch
    {
      >= 18 => new RequestConfiguration
      {
        MaxAdContentRating = MaxAdContentRating.MA,
        TagForChildDirectedTreatment = TagForChildDirectedTreatment.False
      },
      >= 12 => new RequestConfiguration
      {
        MaxAdContentRating = MaxAdContentRating.T,
        TagForChildDirectedTreatment = TagForChildDirectedTreatment.True
      },
      >= 7 => new RequestConfiguration
      {
        MaxAdContentRating = MaxAdContentRating.PG,
        TagForChildDirectedTreatment = TagForChildDirectedTreatment.True
      },
      _ => new RequestConfiguration
      {
        MaxAdContentRating = MaxAdContentRating.G,
        TagForChildDirectedTreatment = TagForChildDirectedTreatment.True
      },
    };
    MobileAds.SetRequestConfiguration(requestConfig);
    if (MobileAds.GetRequestConfiguration() == null)
      print("null2");
    print("Rating: " + requestConfig.MaxAdContentRating.Value);
  }
}
