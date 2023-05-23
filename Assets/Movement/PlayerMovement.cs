using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
  //class variables
  [Header("Objects")]
  public MobileJoystick joystickInput;
  public PlayerMobileInput playerInput;
  public Rigidbody2D rb2d;
  public Animator anim;
  public PolygonCollider2D col;
  public PlayerStats stats;
  public SpriteRenderer color;
  public Transform splash;
  public Transform splashOffset;
  public Slider energySlider;

  [Space(20)]
  //player variables
  private float wait;
  private const float speedRef = 5;
  private float speed;
  private float boostMultiplyer;
  private bool isBoost;
  private float rotationSpeed;
  private bool isJump;
  private bool isStun;
  private float angle;
  private float size;
  private bool boostButtonPressed;
  private float energyAmount;
  private int enemyCount;
  public int drainSpeed;
  private IEnumerator boostCoroutine;
  private bool canRefill;

  private void Start()
  {
    //setup
    speed = speedRef;
    boostMultiplyer = 3f;
    rotationSpeed = 15;
    isBoost = false;
    size = 100;
    energyAmount = 100;
    energySlider.value = energyAmount;
    transform.position = Vector3.zero;
    stats.playerSize = size;
    stats.playerScript = transform;
    isJump = false;
    enemyCount = 0;
    boostCoroutine = RefillEnergyTimer();

    //events
    joystickInput.OnMove += MovePlayer;
    playerInput.OnBoostPressed += BoostOn;
    playerInput.OnBoostReleased += BoostOff;
    playerInput.JumpPlayer += PlayerJump;
  }


  //Position, boost (UI, Refill, Drain)
  private void Update()
  {
    stats.playerPosition = transform.position;
    if (boostButtonPressed)
    {
      DrainEnergySlider();
      if (energyAmount < 0)
      {
        energyAmount = 0;
        BoostOff();
      }
    }
    else
    {
      RefillEnergy();
    }
    energySlider.value = energyAmount;
    stats.energy = energyAmount;
  }
  //Move Player
  private void MovePlayer(Vector2 input)
  {
    if (!isJump && !isStun)
    {
      rb2d.velocity = input * speed;
      angle = Angle(input);
      transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -angle - 90), rotationSpeed * Time.deltaTime);
    }
  }
  //Rotate Player
  public static float Angle(Vector2 vector2)
  {
    if (vector2.x < 0)
    {
      return 360 - (Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg * -1);
    }
    else
    {
      return Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg;
    }
  }


  //Boost
  private void BoostOn()
  {
    boostButtonPressed = true;
    canRefill = false;
    StopCoroutine(boostCoroutine);
    if (!isJump && energyAmount > 0)
    {
      speed = speedRef * boostMultiplyer;
      anim.speed = 1.5f;
      isBoost = true;
    }

  }
  private void BoostOff()
  {
    boostButtonPressed = false;
    boostCoroutine = RefillEnergyTimer();
    StartCoroutine(boostCoroutine);
    if (!isJump)
    {
      speed = speedRef;
      anim.speed = 1;
      isBoost = false;
    }
  }
  private void DrainEnergySlider()
  {
    if (energyAmount > 0)
    {
      energyAmount -= Time.deltaTime * drainSpeed;
    }
  }
  IEnumerator RefillEnergyTimer()
  {
    yield return new WaitForSeconds(2f);
    canRefill = true;
  }
  private void RefillEnergy()
  {
    if (energyAmount < 100 && canRefill)
    {
      energyAmount += Time.deltaTime * drainSpeed * 4;
    }
  }


  //Jump
  private void PlayerJump()
  {
    if (!isStun && energyAmount >= 10)
    {
      //Boost routine
      energyAmount -= 10;
      StartCoroutine(boostCoroutine);
      canRefill = false;
      StopCoroutine(boostCoroutine);

      //Splash routine
      SplashAnim();
      wait = isBoost ? 1 : 1.5f;
      StartCoroutine(SplashDelay());
      col.isTrigger = true;
      anim.SetTrigger("JumpTrigger");
    }

  }
  //called from animation state fro right timing
  public void AnimJump()
  {
    isJump = true;
    color.enabled = false;
  }
  //called from animation state for right timing
  public void AnimSwim()
  {
    isJump = false;
    if (!boostButtonPressed)
    {
      BoostOff();
    }
    else
    {
      BoostOn();
    }
    col.isTrigger = false;
    color.enabled = true;
  }
  public bool GetIsJump()
  {
    return isJump;
  }

  private void SplashAnim()
  {
    splash.position = isBoost ? splashOffset.position : stats.playerPosition;
    splash.rotation = transform.rotation;
    splash.GetComponent<Animator>().Play("Base Layer.Splash", 0, 0);
  }

  IEnumerator SplashDelay()
  {
    yield return new WaitForSeconds(wait);
    SplashAnim();
  }

  //Stun
  public void Stun()
  {
    isStun = true;
    if (isBoost)
    {
      BoostOff();
    }
    playerInput.OnBoostPressed -= BoostOn;
    playerInput.OnBoostReleased -= BoostOff;
    playerInput.JumpPlayer -= PlayerJump;
    StartCoroutine(StunDelay());
  }
  IEnumerator StunDelay()
  {
    yield return new WaitForSeconds(2f);
    isStun = false;
    playerInput.OnBoostPressed += BoostOn;
    playerInput.OnBoostReleased += BoostOff;
    playerInput.JumpPlayer += PlayerJump;
  }


  //Indicator
  public void enemyCounter(bool val)
  {
    enemyCount = val ? enemyCount + 1 : enemyCount - 1;
    if (enemyCount == 0)
    {
      color.color = Color.yellow;
    }
    else if (enemyCount == 1)
    {
      color.color = Color.red;
    }
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    // if (other.transform.name == "Shark")
    // {
    if (!GetIsJump())
    {
      switch (other.transform.GetComponent<Identifier>().fishType)
      {
        case "Predator":
          break;
        case "Prey":
          other.transform.parent.GetComponent<AbstractFactory>().UpdateObject(other.transform);
          break;
        case "Food":
          Destroy(other.gameObject);
          break;
        case "Starfish":
          Destroy(other.gameObject);
          TriggerPower();
          break;
      }
      stats.points += other.transform.GetComponent<Identifier>().value;
    }

    // }
  }


  void TriggerPower()
  {
    // PowerAnimation();
    StartCoroutine(PowerEvent());
  }

  IEnumerator PowerEvent()
  {
    yield return new WaitForSeconds(4f);
    int power = Random.Range(0, 3);
    Debug.Log(power);
  }

  //Reset stat position
  void OnApplicationQuit()
  {
    stats.playerPosition = Vector3.zero;
  }



  // private void GrowPlayer(int sizeIncrease)
  // {
  //   size += sizeIncrease;
  //   float vectorSize = size / 100;
  //   transform.localScale = new Vector3(vectorSize, vectorSize, vectorSize);
  //   stats.playerSize = size;
  // }
}
