using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingHandler : MonoBehaviour
{

  public PlayerMovement playerMovement;
  public MunchSounds munchSounds;
  public ScoreHandler scoreHandler;
  public GameObject skeleton;
  public Transform skeletonParent;
  public AudioSource dodgeSound;
  private GameObject newSkeleton;
  public EnemyList enemyList;
  private Fishes fishName;
  private FishType fishType;
  private Transform otherTransform;
  private Identifier otherIdentifier;
  [SerializeField] private CurrencyCounter coinCounter;
  [SerializeField] private CurrencyCounter gemCounter;
  [SerializeField] private GameObject deathScreen;


  private void Start()
  {
    GameManager.lastFish = Fishes.TREASURE;
    deathScreen.SetActive(false);
  }

  private void OnCollisionStay2D(Collision2D other)
  {
    otherTransform = other.transform;
    otherIdentifier = otherTransform.GetComponent<Identifier>();
    fishType = otherIdentifier.fishType;

    if (!playerMovement.GetIsJump())
    {
      if (fishType == FishType.PREDATOR)
      {
        if (TutorialManager.isInTutorial)
        {
          //transform.Translate(Vector3.zero);
          TutorialManager.death();
          return;
        }
        Death();
      }
    }
  }

  bool eatFish = false;


  private void Death()
  {
    Debug.Log("Death");
    Time.timeScale = 0;
    GameManager.pause();
    transform.GetComponent<PolygonCollider2D>().enabled = false;
    deathScreen.SetActive(true);
  }


  private void OnCollisionEnter2D(Collision2D other)
  {
    if (!playerMovement.GetIsJump())
    {
      otherTransform = other.transform;
      otherIdentifier = otherTransform.GetComponent<Identifier>();
      fishType = otherIdentifier.fishType;
      fishName = otherIdentifier.fishName;
      Vector3 pos = other.transform.position;
      switch (fishType)
      {
        case FishType.PREDATOR:
          print("IsInvincible: " + playerMovement.GetIsInvincible());
          if (playerMovement.GetIsInvincible())
          {
            MissionData.IncrementMission(MissionName.bigSharksEaten);
            AquariumManager.IncrementFish(fishName);
            InstatiateSkeleton(pos, otherTransform.rotation, otherTransform.localScale * (enemyList.scaleModifier.TryGetValue(fishName, out float value0) ? value0 : 0));
            otherTransform.parent.GetComponent<AbstractFactory>().UpdateObject(otherTransform);
            EatEvent(fishName, otherIdentifier.value, pos);
          }
          else
          {
            if (TutorialManager.isInTutorial)
            {
              TutorialManager.death();

              return;
            }
            Death();
          }
          break;
        case FishType.OBJECT:
          break;
        case FishType.PREY:
          if (fishName == Fishes.COIN)
            coinCounter.AddCurrency(Currency.Coin, 20);
          InstatiateSkeleton(pos, otherTransform.rotation, otherTransform.localScale * (enemyList.scaleModifier.TryGetValue(fishName, out float value) ? value : 0));
          otherTransform.parent.GetComponent<AbstractFactory>().UpdateObject(otherTransform);
          EatEvent(fishName, otherIdentifier.value, pos);
          if (TutorialManager.isInTutorial && !eatFish)
          {
            TutorialManager.eatFish?.Invoke();
            eatFish = true;
            return;
          }
          AquariumManager.IncrementFish(fishName);
          break;
        case FishType.FOOD:
          InstatiateSkeleton(pos, otherTransform.rotation, otherTransform.localScale * (enemyList.scaleModifier.TryGetValue(fishName, out value) ? value : 0));
          Destroy(other.gameObject);
          EatEvent(fishName, otherIdentifier.value, pos);
          break;
        case FishType.STARFISH:
          playerMovement.TriggerPower();
          otherTransform.parent.GetComponent<AbstractFactory>().SpawnObject(otherTransform);
          MissionData.IncrementMission(MissionName.starfishesCollected);
          AquariumManager.IncrementFish(Fishes.STARFISH);
          munchSounds.playMunch();
          break;
      }
    }
  }

  bool eatBird = false;
  private void OnTriggerStay2D(Collider2D other)
  {
    otherTransform = other.transform;
    otherIdentifier = otherTransform.GetComponent<Identifier>();
    fishName = otherIdentifier.fishName;

    if (playerMovement.GetIsJump())
    {
      Vector3 pos = otherTransform.position;
      switch (fishName)
      {
        case Fishes.SEAGULL:
          otherTransform.parent.GetComponent<AbstractFactory>().UpdateObject(otherTransform);
          EatEvent(fishName, otherIdentifier.value, pos);
          if (TutorialManager.isInTutorial && !eatBird)
          {
            TutorialManager.eatBird?.Invoke();
            eatBird = true;
            return;
          }
          MissionData.IncrementMission(MissionName.birdsEaten);
          AquariumManager.IncrementFish(Fishes.SEAGULL);
          break;
          //case "People":
          //  Destroy(other.gameObject);
          //  EatEvent(fishName, otherIdentifier.value, pos);
          //  break;
      }

      //Slo-mo
      //else if (fishType == "Predator")
      //{
      //  Time.timeScale = 0.6f;
      //}

    }
  }
  //private void OnTriggerExit2D(Collider2D other)
  //{
  //  //Slo-mo
  //  otherTransform = other.transform;
  //  otherIdentifier = otherTransform.GetComponent<Identifier>();
  //  fishName = otherIdentifier.fishName;
  //  if (fishType == "Predator" && playerMovement.GetIsJump())
  //  {
  //    Time.timeScale = 1f;
  //  }
  //}
  private void OnTriggerEnter2D(Collider2D other)
  {
    otherTransform = other.transform;
    otherIdentifier = otherTransform.GetComponent<Identifier>();
    fishType = otherIdentifier.fishType;
    fishName = otherIdentifier.fishName;
    switch (fishType)
    {
      case FishType.STUN:
        if (!playerMovement.GetIsJump() && !playerMovement.GetIsInvincible())
        {
          GameManager.eventText("Stung", 1f);
          other.transform.parent.GetComponent<JellyMovement>().Shock();
          playerMovement.Stun();
          MissionData.IncrementMission(MissionName.timesStung);
        }
        break;
      case FishType.INK:
        if (!playerMovement.GetIsJump() && !playerMovement.GetIsInvincible())
        {
          GameManager.eventText("Inked", 1f);
          other.enabled = false;
          playerMovement.Ink();
          MissionData.IncrementMission(MissionName.timesInked);
        }
        break;
      case FishType.COIN:
        if (!playerMovement.GetIsJump())
        {
          other.transform.GetComponent<CoinScript>().Collected();
          coinCounter.AddCurrency(Currency.Coin, 1);
          MissionData.IncrementMission(MissionName.coinsCollected);
        }
        break;
      case FishType.PREDATOR:
        if (playerMovement.GetIsJump() && !GameManager.dodgeHelper.Contains(otherIdentifier.id))
        {
          //SloMo
          dodgeSound.Play();
          GameManager.dodgeHelper.Add(otherIdentifier.id);
          //Time.timeScale = 0.6f;
          MissionData.IncrementMission(MissionName.bigSharkDodges);
        }
        break;
      case FishType.TREASURE:
        if (!playerMovement.GetIsJump())
        {
          print("treasure!");
          MissionData.IncrementMission(MissionName.treasureCollected);
          other.transform.GetComponent<Treasure>().Collected();
          coinCounter.AddCurrency(Currency.Coin, 100);
          gemCounter.AddCurrency(Currency.Gem, 1);
          AquariumManager.IncrementFish(Fishes.TREASURE);
        }
        break;
    }

  }

  private void EatEvent(Fishes fishName, int value, Vector3 pos)
  {
    playerMovement.AddEnergy();
    scoreHandler.UpdateMultiplyer(fishName, pos);
    scoreHandler.UpdateLastFish(fishName);
    scoreHandler.AddPoints(value);
    munchSounds.playMunch();
  }

  private void InstatiateSkeleton(Vector3 position, Quaternion rotation, Vector3 scale)
  {
    newSkeleton = Instantiate(skeleton, position, rotation, skeletonParent);
    newSkeleton.transform.localScale = scale;
  }
}
