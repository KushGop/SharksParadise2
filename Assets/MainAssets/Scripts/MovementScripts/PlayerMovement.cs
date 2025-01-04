using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
  [Header("Shock")]
  [SerializeField] GameObject shockParent;
  [SerializeField] Animator[] shocks;

  [Header("Controls")]
  [SerializeField] private ControlToggle controlManager;
  [SerializeField] private MobileJoystick joystick;
  [SerializeField] private Image boostJoystick;
  [SerializeField] private Image jumpJoystick;
  //private Control controlRight;
  //private Control controlLeft;
  [Space]
  //class variables
  [Header("Objects")]
  public Rigidbody2D rb2d;
  public Animator anim;
  public Animator hatAnim;
  public PolygonCollider2D col;
  public PlayerStats stats;
  public SpriteRenderer color;
  public SpriteRenderer glow0;
  public SpriteRenderer glow1;
  public SpriteRenderer glow2;
  public Transform splash;
  public Transform splashOffset;
  public Transform newPoints;
  public MunchSounds sounds;
  public AudioSource splashSound;
  public AudioSource diveSound;
  public ScoreHandler scoreHandler;
  private SpriteRenderer spriteRenderer;
  public Transform trails;
  public AudioSource boostSound;
  private Color[] colors;
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

  private float fishEnergy;

  [Header("Powers")]
  [SerializeField] private SpriteRenderer playerSprite;
  [SerializeField] private TextMeshProUGUI textColor;

  [SerializeField] GameObject RedCirclePrefab;
  [SerializeField] Transform RedCircleOrigin;

  [Header("Hat")]
  [SerializeField] HatLoader hatLoader;
  [SerializeField] Closet_ScritableObject hats;
  SpriteRenderer hatRenderer;

  //Reset start position
  void Awake()
  {
    stats.playerPosition = Vector3.zero;
  }

  private void Start()
  {
    //upgrades
    baseSpeed = 10 + (UpgradesManager.upgradesData.upgrades[UpgradeList.baseSpeed] * 0.1f);
    boostSpeed = 2 + (UpgradesManager.upgradesData.upgrades[UpgradeList.boostSpeed] * 0.1f);
    boostCost = 15 - (UpgradesManager.upgradesData.upgrades[UpgradeList.boostCost] * 0.1f);
    jumpCost = 15 - (UpgradesManager.upgradesData.upgrades[UpgradeList.jumpCost] * 0.1f);
    refillSpeed = 30 + (UpgradesManager.upgradesData.upgrades[UpgradeList.refillSpeed] * 0.1f);
    refillDelay = 2 - (UpgradesManager.upgradesData.upgrades[UpgradeList.refillDelay] * 0.1f);
    powerTime = 10 + (UpgradesManager.upgradesData.upgrades[UpgradeList.powerTime] * 0.1f);

    fishEnergy = 10;

    //setup
    speed = baseSpeed;
    rotationSpeed = 15;
    isBoost = false;
    size = 100;
    energyAmount = 100;
    //energySlider.value = energyAmount;
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
    Color c = Color.green;
    color.color = c;
    c.a = 0.35f;
    glow0.color = c;
    glow1.color = c;
    glow2.color = c;

    shockParent.SetActive(false);
    foreach (Animator s in shocks)
      s.speed = 0;
    //controlRight = controlManager.controlRight;
    //controlLeft = controlManager.controlLeft;

    colors = new Color[] { Color.cyan, Color.blue, Color.magenta, Color.red, Color.yellow, Color.green };

    //hat
    GameObject hat = Instantiate(hats.hatDictionary[hatLoader.GetHat()], transform);
    hatRenderer = hat.GetComponent<SpriteRenderer>();
    hatAnim = hat.GetComponent<Animator>();

    //events
    //controlLeft.mobileJoystick.OnMove += MovePlayer;
    //controlRight.mobileJoystick.OnMove += MovePlayer;
    joystick.OnMove += MovePlayer;
    joystick.OnBoostPressed += BoostOn;
    joystick.OnBoostReleased += BoostOff;
    joystick.JumpPlayer += PlayerJump;
  }
  private void OnDestroy()
  {
    //controlLeft.mobileJoystick.OnMove -= MovePlayer;
    //controlRight.mobileJoystick.OnMove -= MovePlayer;
    joystick.OnMove -= MovePlayer;
    joystick.OnBoostPressed -= BoostOn;
    joystick.OnBoostReleased -= BoostOff;
    joystick.JumpPlayer -= PlayerJump;
  }

  #region Tutorial
  public void EnableEnergy()
  {
    boostCost = 10;
    jumpCost = 10;
  }
  public void DisableEnergy()
  {
    boostCost = 0;
    jumpCost = 0;
    speed = 10;
    refillSpeed = 30;
    refillDelay = 2;
  }
  #endregion

  //Position, boost (UI, Refill, Drain)
  private void Update()
  {
    boostJoystick.fillAmount = energyAmount / 100;
    stats.playerPosition = transform.position;
    if (boostButtonPressed && !isUnlimitedBoost)
    {
      DrainEnergySlider();
      if (energyAmount < 0)
      {
        energyAmount = 0;
        BoostOff();
        joystick.BoostReleased();
      }
    }
    else
    {
      RefillEnergy();
    }
    //energySlider.value = energyAmount;
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
    EnemyCounter(0);
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
      anim.SetBool("Boost", true);
      //anim.speed = 1.5f;
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
      //anim.speed = 1;
      anim.SetBool("Boost", false);
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
  public void AddEnergy()
  {
    if (energyAmount + fishEnergy > 100)
      energyAmount = 100;
    else
      energyAmount += fishEnergy;

  }
  #endregion

  #region Jump
  //Jump
  private void PlayerJump()
  {
    if (!isStun && energyAmount >= jumpCost && !isJump)
    {
      //Boost routine
      energyAmount -= jumpCost;
      StartCoroutine(boostCoroutine);
      canRefill = false;
      StopCoroutine(boostCoroutine);

      //Splash routine
      splashSound.Play();
      SplashAnim();
      StartCoroutine(SplashDelay(1f));
      col.isTrigger = true;
      anim.SetTrigger("JumpTrigger");
      hatAnim.SetTrigger("JumpTrigger");
    }

  }
  //called from animation state fro right timing
  public void AnimJump()
  {
    //speed = baseSpeed;
    //speed = baseSpeed * boostSpeed;
    //anim.speed = 1f;
    StartCoroutine(FrameDelay());
  }
  private IEnumerator FrameDelay()
  {
    yield return null;
    isJump = true;
    EnemyCounter(0);
    color.enabled = false;
  }

  public bool GetIsJump()
  {
    return isJump;
  }

  private void SplashAnim()
  {
    JumpActions();
    splash.SetPositionAndRotation(splashOffset.position, transform.rotation);
    splash.GetComponent<Animator>().Play("Base Layer.Splash", 0, 0);
  }

  IEnumerator SplashDelay(float wait)
  {
    yield return new WaitForSeconds(wait);
    if (!isBoost)
    {
      speed = baseSpeed;
      //anim.speed = 1.5f;
    }
    SplashAnim();
    diveSound.Play();
  }

  private void JumpActions()
  {
    GameManager.dodgeHelper.Clear();
    //slomo
    Time.timeScale = 1f;
    spriteRenderer.sortingLayerName = spriteRenderer.sortingLayerName == "Jump" ? "Player" : "Jump";
    hatRenderer.sortingLayerName = hatRenderer.sortingLayerName == "Jump" ? "Player" : "Jump";
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
      shockParent.SetActive(true);
      foreach (Animator s in shocks)
      {
        s.speed = 1;
        s.SetFloat("Offset", Random.value * 3);
        s.Play("PlayerShock");
      }
      if (isBoost)
      {
        BoostOff();
      }
      joystick.OnBoostPressed -= BoostOn;
      joystick.OnBoostReleased -= BoostOff;
      joystick.JumpPlayer -= PlayerJump;
      StopCoroutine(StunDelay());
      StartCoroutine(StunDelay());
    }

  }
  IEnumerator StunDelay()
  {
    yield return new WaitForSeconds(2f);
    isStun = false;
    joystick.OnBoostPressed += BoostOn;
    joystick.OnBoostReleased += BoostOff;
    joystick.JumpPlayer += PlayerJump;
    shockParent.SetActive(false);
    foreach (Animator s in shocks)
      s.speed = 0;
  }
  #endregion

  #region Power
  public void TriggerPower()
  {
    StartCoroutine(PowerEvent(Random.Range(0, 3)));
  }
  public void TriggerPower(int i)
  {
    StartCoroutine(PowerEvent(i));
  }

  IEnumerator PowerEvent(int power)
  {
    GameManager.eventText(GameManager.powers[power], 3f);
    switch (power)
    {
      case 0://Invincible
        isInvincible = true;
        //UpdateEnemies("Prey");
        StopCoroutine("SpriteRotateColors");
        StartCoroutine(SpriteRotateColors(powerTime, playerSprite));
        break;
      case 1://Double Points
        scoreHandler.starfishMultiplyer = 2;
        StopCoroutine("TextRotateColors");
        StartCoroutine(TextRotateColors(powerTime, textColor));
        break;
      case 2://Unlimited Boost
        StopCoroutine("ImageRotateColors");
        StopCoroutine("ImageRotateColors");
        StartCoroutine(ImageRotateColors(powerTime, boostJoystick));
        //StartCoroutine(ImageRotateColors(powerTime, controlRight.boostSprite));
        isUnlimitedBoost = true;
        break;
    }
    yield return new WaitForSeconds(powerTime + 0.1f);
    print("Power end");
    switch (power)
    {
      case 0://Invincible
        isInvincible = false;
        //UpdateEnemies("Predator");
        break;
      case 1://Double Points
        scoreHandler.starfishMultiplyer = 1;
        break;
      case 2://Unlimited Boost
        isUnlimitedBoost = false;
        break;
    }
  }

  //private void UpdateEnemies(string fishType)
  //{
  //  foreach (GameObject enemy in enemies)
  //  {
  //    foreach (Transform e in enemy.transform)
  //    {
  //      e.GetComponent<Identifier>().fishType = fishType;
  //    }
  //  }
  //}

  #region RotateColors
  IEnumerator ImageRotateColors(float waitTime, Image sprite)
  {
    i = 0;
    float timePassed = 0f;
    Color startColor = sprite.color;
    StartCoroutine(ColorDelay());
    while (timePassed < waitTime - 3)
    {
      sprite.color = Color.Lerp(sprite.color, colors[i % 6], waitTime * Time.deltaTime);
      timePassed += Time.deltaTime;
      yield return null;
    }
    StopCoroutine(ColorDelay());
    for (int j = 0; j < 5; j++)
    {
      sprite.color = startColor;
      yield return new WaitForSeconds(0.3f);
      sprite.color = Color.red;
      yield return new WaitForSeconds(0.3f);
    }

    sprite.color = startColor;
    yield return null;
  }
  IEnumerator SpriteRotateColors(float waitTime, SpriteRenderer sprite)
  {
    i = 0;
    float timePassed = 0f;
    Color startColor = Color.white;
    StartCoroutine(ColorDelay());
    while (timePassed < waitTime - 3)
    {
      sprite.color = Color.Lerp(sprite.color, colors[i % 6], waitTime * Time.deltaTime);
      timePassed += Time.deltaTime;
      yield return null;
    }
    StopCoroutine(ColorDelay());
    for (int j = 0; j < 5; j++)
    {
      sprite.color = startColor;
      yield return new WaitForSeconds(0.3f);
      sprite.color = Color.red;
      yield return new WaitForSeconds(0.3f);
    }

    sprite.color = startColor;
    yield return null;
  }

  IEnumerator TextRotateColors(float waitTime, TextMeshProUGUI sprite)
  {
    i = 0;
    float timePassed = 0f;
    Color startColor = sprite.color;
    StartCoroutine(ColorDelay());
    while (timePassed < waitTime - 3)
    {
      sprite.color = Color.Lerp(sprite.color, colors[i % 6], waitTime * Time.deltaTime);
      timePassed += Time.deltaTime;
      yield return null;
    }
    StopCoroutine(ColorDelay());
    for (int j = 0; j < 5; j++)
    {
      sprite.color = startColor;
      yield return new WaitForSeconds(0.3f);
      sprite.color = Color.red;
      yield return new WaitForSeconds(0.3f);
    }

    sprite.color = startColor;
    yield return null;
  }
  IEnumerator ColorDelay()
  {
    yield return new WaitForSeconds(0.2f);
    i++;
    StartCoroutine(ColorDelay());
  }
  #endregion
  public bool GetIsInvincible()
  {
    return isInvincible;
  }
  public void SetIsInvincible(bool i)
  {
    isInvincible = i;
  }

  #endregion

  //Indicator
  public void EnemyCounter(int val)
  {
    if (val == 1)
    {
      Instantiate(RedCirclePrefab, RedCircleOrigin);
    }
    enemyCount += val;
    if (enemyCount == 0 || isJump)
    {
      Color c = Color.green;
      color.color = c;
      c.a = 0.35f;
      glow0.color = c;
      glow1.color = c;
      glow2.color = c;
    }
    else if (enemyCount >= 1)
    {
      Color c = Color.red;
      color.color = c;
      c.a = 0.35f;
      glow0.color = c;
      glow1.color = c;
      glow2.color = c;
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
