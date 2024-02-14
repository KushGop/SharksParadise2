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
  private GameObject newSkeleton;
  public EnemyList enemyList;
  private string fishType, fishName;
  private Transform otherTransform;
  private Identifier otherIdentifier;
  [SerializeField] private CoinCounter coinCounter;
  [SerializeField] private GameObject deathScreen;


  private void Start()
  {
    GameManager.lastFish = "null";
    deathScreen.SetActive(false);
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    otherTransform = other.transform;
    otherIdentifier = otherTransform.GetComponent<Identifier>();
    fishType = otherIdentifier.fishType;
    fishName = otherIdentifier.fishName;

    if (!playerMovement.GetIsJump())
    {
      if (fishType == "Predator")
      {
        Debug.Log("Death");
        Time.timeScale = 0;
        transform.GetComponent<PolygonCollider2D>().enabled = false;
        deathScreen.SetActive(true);
      }
      else if (fishType == "Object")
      {
      }
      else
      {
        Vector3 pos = other.transform.position;

        switch (fishType)
        {
          case "Prey":
            if (fishName == "BigShark")
            {
              MissionData.IncrementMission(MissionName.bigSharksEaten);
            }
            InstatiateSkeleton(pos, otherTransform.rotation, otherTransform.localScale * (enemyList.scaleModifier.TryGetValue(fishName, out float value) ? value : 0));
            otherTransform.parent.GetComponent<AbstractFactory>().UpdateObject(otherTransform);
            break;
          case "Food":
            InstatiateSkeleton(pos, otherTransform.rotation, otherTransform.localScale * (enemyList.scaleModifier.TryGetValue(fishName, out value) ? value : 0));
            Destroy(other.gameObject);
            break;
          case "Starfish":
            playerMovement.TriggerPower();
            Destroy(other.gameObject);
            MissionData.IncrementMission(MissionName.starfishesCollected);
            break;
        }
        EatEvent(fishName, otherIdentifier.value, pos);
      }
    }
  }

  private void OnTriggerStay2D(Collider2D other)
  {
    otherTransform = other.transform;
    otherIdentifier = otherTransform.GetComponent<Identifier>();
    fishName = otherIdentifier.fishName;

    if (playerMovement.GetIsJump())
    {
      Vector3 pos = otherTransform.position;
      if (fishName == "Bird")
      {
        otherTransform.parent.GetComponent<AbstractFactory>().UpdateObject(otherTransform);
        EatEvent(fishName, otherIdentifier.value, pos);
        MissionData.IncrementMission(MissionName.birdsEaten);
        EatEvent(fishName, otherIdentifier.value, pos);
      }
      else if (fishName == "People")
      {
        Destroy(other.gameObject);
        EatEvent(fishName, otherIdentifier.value, pos);
      }
    }
  }
  private void OnTriggerEnter2D(Collider2D other)
  {
    otherTransform = other.transform;
    otherIdentifier = otherTransform.GetComponent<Identifier>();
    fishType = otherIdentifier.fishType;
    fishName = otherIdentifier.fishName;
    switch (fishType)
    {
      case "Stun":
        if (!playerMovement.GetIsJump())
        {
          playerMovement.Stun();
          MissionData.IncrementMission(MissionName.timesStung);
        }
        break;
      case "Ink":
        if (!playerMovement.GetIsJump())
        {
          other.enabled = false;
        }
        playerMovement.Ink();
        MissionData.IncrementMission(MissionName.timesInked);
        break;
      case "Coin":
        if (!playerMovement.GetIsJump())
        {
          other.transform.GetComponent<CoinScript>().Collected();
          coinCounter.AddCoin(1);
          MissionData.IncrementMission(MissionName.coinsCollected);
        }
        break;
      case "Predator":
        if (playerMovement.GetIsJump())
        {
          MissionData.IncrementMission(MissionName.bigSharkDodges);
        }
        break;
    }

  }

  private void EatEvent(string fishName, int value, Vector3 pos)
  {
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
