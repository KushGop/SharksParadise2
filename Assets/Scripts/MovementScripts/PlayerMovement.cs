using System.Collections;
using UnityEngine;
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
  public Transform newPoints;
  public MunchSounds sounds;
  public AudioSource splashSound;
  public AudioSource diveSound;
  public ScoreHandler scoreHandler;
  private SpriteRenderer spriteRenderer;
  public Transform trails;
  public AudioSource boostSound;
  private Color[] colors;
  public GameObject[] enemies;
  public GameObject inkOverlayParent;
  public GameObject inkFactory;


  [Space(20)]
  //player variables
  private float speed;
  private bool isBoost;
  private float rotationSpeed;
  private bool isJump;
  private bool isStun;
  private float angle;
  private float size;
  private bool boostButtonPressed;
  private float energyAmount;
  private int enemyCount;
  private IEnumerator boostCoroutine;
  private bool canRefill;
  private bool isOverBoat;
  private bool isInvincible, isUnlimitedBoost;
  private int i = 0;

  //upgrades
  private float baseSpeed;
  private float boostSpeed;
  private float boostCost;
  private float jumpCost;
  private float refillSpeed;
  private float refillDelay;
  private float powerTime;

  //Reset start position
  void Awake()
  {
    stats.playerPosition = Vector3.zero;
  }

  private void Start()
  {
    //upgrades
    baseSpeed = 5 + (UpgradesManager.upgradesData.upgrades[UpgradeList.baseSpeed] * 0.1f);
    boostSpeed = 2 + (UpgradesManager.upgradesData.upgrades[UpgradeList.boostSpeed] * 0.1f);
    boostCost = 5 - (UpgradesManager.upgradesData.upgrades[UpgradeList.boostCost] * 0.1f);
    jumpCost = 10 - (UpgradesManager.upgradesData.upgrades[UpgradeList.jumpCost] * 0.1f);
    refillSpeed = 40 + (UpgradesManager.upgradesData.upgrades[UpgradeList.refillSpeed] * 0.1f);
    refillDelay = 2 - (UpgradesManager.upgradesData.upgrades[UpgradeList.refillDelay] * 0.1f);
    powerTime = 10 + (UpgradesManager.upgradesData.upgrades[UpgradeList.powerTime] * 0.1f);

    //setup
    speed = baseSpeed;
    rotationSpeed = 15;
    isBoost = false;
    size = 100;
    energyAmount = 100;
    energySlider.value = energyAmount;
    transform.position = Vector3.zero;
    stats.playerSize = size;
    stats.playerScript = transform;
    stats.playerPosition = Vector3.zero;
    isJump = false;
    enemyCount = 0;
    boostCoroutine = RefillEnergyTimer();
    spriteRenderer = transform.GetComponent<SpriteRenderer>();
    isInvincible = false;
    isUnlimitedBoost = false;





    colors = new Color[] { Color.cyan, Color.blue, Color.magenta, Color.red, Color.yellow, Color.green };



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
    if (boostButtonPressed && !isUnlimitedBoost)
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
  #region Movement
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
  #endregion

  #region Boat
  //Boat
  public void SetIsOverBoat(bool b)
  {
    isOverBoat = b;
  }
  #endregion

  #region Boost
  //Boost
  private void BoostOn()
  {
    boostSound.Play();
    boostButtonPressed = true;
    canRefill = false;
    StopCoroutine(boostCoroutine);
    if (!isJump && energyAmount > 0)
    {
      speed = baseSpeed * boostSpeed;
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
      speed = baseSpeed;
      anim.speed = 1;
      isBoost = false;
    }
  }
  private void DrainEnergySlider()
  {
    if (energyAmount > 0)
    {
      energyAmount -= Time.deltaTime * boostCost;
    }
  }
  IEnumerator RefillEnergyTimer()
  {
    yield return new WaitForSeconds(refillDelay);
    canRefill = true;
  }
  private void RefillEnergy()
  {
    if (energyAmount < 100 && canRefill)
    {
      energyAmount += Time.deltaTime * refillSpeed;
    }
  }
  #endregion

  #region Jump
  //Jump
  private void PlayerJump()
  {
    if (!isStun && energyAmount >= 10 && !isJump)
    {
      //Boost routine
      energyAmount -= jumpCost;
      StartCoroutine(boostCoroutine);
      canRefill = false;
      StopCoroutine(boostCoroutine);

      //Splash routine
      splashSound.Play();
      SplashAnim();
      StartCoroutine(SplashDelay(1.5f));
      col.isTrigger = true;
      anim.SetTrigger("JumpTrigger");
    }

  }
  //called from animation state fro right timing
  public void AnimJump()
  {
    speed = baseSpeed * boostSpeed;
    anim.speed = 1.5f;
    StartCoroutine(FrameDelay());
  }
  private IEnumerator FrameDelay()
  {
    yield return null;
    isJump = true;
    color.enabled = false;
  }

  public bool GetIsJump()
  {
    return isJump;
  }

  private void SplashAnim()
  {
    JumpActions();
    splash.SetPositionAndRotation(isBoost ? splashOffset.position : stats.playerPosition, transform.rotation);
    splash.GetComponent<Animator>().Play("Base Layer.Splash", 0, 0);
  }

  IEnumerator SplashDelay(float wait)
  {
    yield return new WaitForSeconds(wait);
    if (!isBoost)
    {
      speed = baseSpeed;
      anim.speed = 1f;
    }
    SplashAnim();
    diveSound.Play();
  }

  private void JumpActions()
  {
    GameManager.dodgeHelper.Clear();
    Time.timeScale = 1f;
    spriteRenderer.sortingLayerName = spriteRenderer.sortingLayerName == "Jump" ? "Player" : "Jump";
    trails.GetComponent<TrailScript>().changeParent();
  }
  #endregion

  #region Stun
  //Stun
  public void Stun()
  {
    if (!isInvincible)
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

  }
  IEnumerator StunDelay()
  {
    yield return new WaitForSeconds(2f);
    isStun = false;
    playerInput.OnBoostPressed += BoostOn;
    playerInput.OnBoostReleased += BoostOff;
    playerInput.JumpPlayer += PlayerJump;
  }
  #endregion

  #region Power
  public void TriggerPower()
  {
    // PowerAnimation();
    StartCoroutine(PowerEvent(Random.Range(0, 3)));
  }
  public void TriggerPower(int i)
  {
    StartCoroutine(PowerEvent(i));
  }

  IEnumerator PowerEvent(int power)
  {
    switch (power)
    {
      case 0://Invincible
        Debug.Log("Invincible");
        isInvincible = true;
        UpdateEnemies("Prey");
        StartCoroutine(Invincible());
        break;
      case 1://Double Points
        Debug.Log("Double Points");
        scoreHandler.starfishMultiplyer = 2;
        break;
      case 2://Unlimited Boost
        Debug.Log("Unlimited Boost");
        isUnlimitedBoost = true;
        break;
    }
    yield return new WaitForSeconds(powerTime);
    switch (power)
    {
      case 0://Invincible
        Debug.Log("Invincible End");
        isInvincible = false;
        UpdateEnemies("Predator");
        break;
      case 1://Double Points
        Debug.Log("Double Points End");
        scoreHandler.starfishMultiplyer = 1;
        break;
      case 2://Unlimited Boost
        Debug.Log("Unlimited Boost End");
        isUnlimitedBoost = false;
        break;
    }
  }

  private void UpdateEnemies(string fishType)
  {
    foreach (GameObject enemy in enemies)
    {
      foreach (Transform e in enemy.transform)
      {
        e.GetComponent<Identifier>().fishType = fishType;
      }
    }
  }
  IEnumerator Invincible()
  {
    i = 0;
    float waitTime = 10f;
    float timePassed = 0f;
    Color color = spriteRenderer.color;
    StartCoroutine(ColorDelay());
    while (timePassed < waitTime)
    {
      spriteRenderer.color = Color.Lerp(spriteRenderer.color, colors[i % 6], waitTime * Time.deltaTime);
      timePassed += Time.deltaTime;
      yield return null;
    }
    StopCoroutine(ColorDelay());
    spriteRenderer.color = color;
    yield return null;
  }
  public bool GetIsInvincible()
  {
    return isInvincible;
  }
  IEnumerator ColorDelay()
  {
    yield return new WaitForSeconds(0.2f);
    i++;
    StartCoroutine(ColorDelay());
  }
  #endregion

  //Indicator
  public void EnemyCounter(bool val)
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

  internal void Ink()
  {
    Vector3 pos = transform.position;
    pos.z = 0;
    GameObject newOverlay = Instantiate(inkFactory, pos, new Quaternion(), inkOverlayParent.transform);
    Destroy(newOverlay, 5f);
  }



  // private void GrowPlayer(int sizeIncrease)
  // {
  //   size += sizeIncrease;
  //   float vectorSize = size / 100;
  //   transform.localScale = new Vector3(vectorSize, vectorSize, vectorSize);
  //   stats.playerSize = size;
  // }
}
