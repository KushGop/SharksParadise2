using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeCard : MonoBehaviour
{

  [SerializeField] FishCard oldCard;
  [SerializeField] FishCard newCard;
  [SerializeField] TextMeshProUGUI cost;

  private void Start()
  {
    AquariumManager.upgradeCard += SetCards;
  }
  private void OnDestroy()
  {
    AquariumManager.upgradeCard -= SetCards;
  }

  private void SetCards(Fishes fish, FishCard_S card)
  {
    cost.text = "<sprite name=\"Gem\">*" + EnemyList.rarityGemCost[card.rarity][card.level].ToString();
    oldCard.CardInit(fish, card);
    card.level++;
    card.count = 0;
    newCard.CardInit(fish, card);
  }
}
