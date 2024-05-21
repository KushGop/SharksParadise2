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
  private string fishType, fishName;
  private Transform otherTransform;
  private Identifier otherIdentifier;
  [SerializeField] private CurrencyCounter coinCounter;
  [SerializeField] private CurrencyCounter gemCounter;
  [SerializeField] private GameObject deathScreen;


  private void Start()
  {
    GameManager.lastFish = "null";
    deathScreen.SetActive(false);
  }

  private void OnCollisionStay2D(Collision2D other)
  {
    otherTransform = other.transform;
    otherIdentifier = otherTransform.GetComponent<Identifier>();
    fishType = otherIdentifier.fishType;

    if (!playerMovement.GetIsJump())
    {
      if (fishType == "Predator")
      {
        if (TutorialManager.isInTutorial)
        {
          TutorialManager.death();
          return;
        }
        Debug.Log("Death");
        Time.timeScale = 0;
        transform.GetComponent<PolygonCollider2D>().enabled = false;
        deathScreen.SetActive(true);
      }
      else if (fishType == "TutorialPredator")
      {
      }
    }
  }

  bool eatFish = false;

  private void OnCollisionEnter2D(Collision2D other)
  {
    otherTransform = other.transform;
    otherIdentifier = otherTransform.GetComponent<Identifier>();
    fishType = otherIdentifier.fishType;
    fishName = otherIdentifier.fishName;

    if (!playerMovement.GetIsJump())
    {
      Vector3 pos = other.transform.position;
      switch (fishType)
      {
        case "Predator":
          print("IsInvincible: " + playerMovement.GetIsInvincible());
          if (playerMovement.GetIsInvincible())
          {
            MissionData.IncrementMission(MissionName.bigSharksEaten);
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
            Debug.Log("Death");
            Time.timeScale = 0;
            transform.GetComponent<PolygonCollider2D>().enabled = false;
            deathScreen.SetActive(true);
          }
          break;
        case "Object":
          break;
        case "Prey":
          if (fishName == "CoinFish")
            coinCounter.AddCurrency(Currency.Coin, 20);
          InstatiateSkeleton(pos, otherTransform.rotation, otherTransform.localScale * (enemyList.scaleModifier.TryGetValue(fishName, out float value) ? value : 0));
          otherTransform.parent.GetComponent<AbstractFactory>().UpdateObject(otherTransform);
          EatEvent(fishName, otherIdentifier.value, pos);
          if (TutorialManager.isInTutorial && !eatFish)
          {
            TutorialManager.eatFish?.Invoke();
            eatFish = true;
          }
          break;
        case "Food":
          InstatiateSkeleton(pos, otherTransform.rotation, otherTransform.localScale * (enemyList.scaleModifier.TryGetValue(fishName, out value) ? value : 0));
          Destroy(other.gameObject);
          EatEvent(fishName, otherIdentifier.value, pos);
          break;
        case "Starfish":
          playerMovement.TriggerPower();
          otherTransform.parent.GetComponent<AbstractFactory>().SpawnObject(otherTransform);
          MissionData.IncrementMission(MissionName.starfishesCollected);
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
        case "Bird":
          otherTransform.parent.GetComponent<AbstractFactory>().UpdateObject(otherTransform);
          EatEvent(fishName, otherIdentifier.value, pos);
          if (TutorialManager.isInTutorial && !eatBird)
          {
            TutorialManager.eatBird?.Invoke();
            eatBird = true;
            return;
          }
          MissionData.IncrementMission(MissionName.birdsEaten);
          break;
        case "People":
          Destroy(other.gameObject);
          EatEvent(fishName, otherIdentifier.value, pos);
          break;
        case "TutorialBird":
          TutorialManager.eatBird();
          break;
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
      case "Stun":
        if (!playerMovement.GetIsJump() && !playerMovement.GetIsInvincible())
        {
          GameManager.eventText("Stung", 1f);
          GameManager.stung();
          playerMovement.Stun();
          MissionData.IncrementMission(MissionName.timesStung);
        }
        break;
      case "Ink":
        if (!playerMovement.GetIsJump() && !playerMovement.GetIsInvincible())
        {
          GameManager.eventText("Inked", 1f);
          other.enabled = false;
          playerMovement.Ink();
          MissionData.IncrementMission(MissionName.timesInked);
        }
        break;
      case "Coin":
        if (!playerMovement.GetIsJump())
        {
          other.transform.GetComponent<CoinScript>().Collected();
          coinCounter.AddCurrency(Currency.Coin, 1);
          MissionData.IncrementMission(MissionName.coinsCollected);
        }
        break;
      case "Predator":
        if (playerMovement.GetIsJump() && !GameManager.dodgeHelper.Contains(otherIdentifier.id))
        {
          //SloMo
          dodgeSound.Play();
          GameManager.dodgeHelper.Add(otherIdentifier.id);
          //Time.timeScale = 0.6f;
          MissionData.IncrementMission(MissionName.bigSharkDodges);
        }
        break;
      case "Treasure":
        if (!playerMovement.GetIsJump())
        {
          print("treasure!");
          MissionData.IncrementMission(MissionName.treasureCollected);
          other.transform.GetComponent<Treasure>().Collected();
          coinCounter.AddCurrency(Currency.Coin, 100);
          gemCounter.AddCurrency(Currency.Gem, 1);
        }
        break;
    }

  }

  private void EatEvent(string fishName, int value, Vector3 pos)
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
