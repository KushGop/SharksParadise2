using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
  [Header("Extra Life")]
  [SerializeField] Animator extraLife;

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
  //private IEnumerator boostCoroutine;
  //private bool canRefill;
  private bool isOverBoat;
  private bool isInvincible, isSpeedBoost;
  private int i = 0;

  //upgrades
  private float baseSpeed;
  private float boostSpeed;
  //private float boostCost;
  //private float jumpCost;
  //private float refillSpeed;
  //private float refillDelay;

  private float fishEnergy;

  [Header("Powers")]
  [SerializeField] private UnityEngine.Rendering.SortingGroup playerSpriteGroup;
  [SerializeField] private TextMeshProUGUI textColor;
  [SerializeField] GameObject invinsiblePower;
  [SerializeField] GameObject speedPower;
  [SerializeField] GameObject multiplyerPower;
  [SerializeField] GameObject invinsibleBarr;
  [SerializeField] GameObject speedBarr;
  [SerializeField] GameObject multiBarr;
  [SerializeField] Sprite[] barrs;
  Coroutine invisEvent, speedEvent, multiEvent;
  [Header("Arrows")]
  [SerializeField] GameObject RedCirclePrefab;
  [SerializeField] GameObject RedArrowPrefab;
  [SerializeField] GameObject TreasureArrowPrefab;
  [SerializeField] GameObject StarfishArrowPrefab;
  [SerializeField] Transform RedCircleOrigin;
  [SerializeField] Transform ArrowOrigin;
  Dictionary<Transform, GameObject> arrows;

  [Header("Spark")]
  [SerializeField] GameObject SparkPrefab;
  [SerializeField] Transform rightWayUp;


  [Header("Hat")]
  [SerializeField] Transform hatParent;
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
    //aquarium
    /*
     *coin fish, starfish, treasure 
     */

    //upgrades
    baseSpeed = 10;// + (UpgradesManager.upgradesData.upgrades[UpgradeList.baseSpeed] * 0.1f);
    boostSpeed = 2;// + (UpgradesManager.upgradesData.upgrades[UpgradeList.boostSpeed] * 0.1f);
    //boostCost = 15 - (UpgradesManager.upgradesData.upgrades[UpgradeList.boostCost] * 0.1f);
    //jumpCost = 15 - (UpgradesManager.upgradesData.upgrades[UpgradeList.jumpCost] * 0.1f);
    //refillSpeed = 30 + (UpgradesManager.upgradesData.upgrades[UpgradeList.refillSpeed] * 0.1f);
    //refillDelay = 2 - (UpgradesManager.upgradesData.upgrades[UpgradeList.refillDelay] * 0.1f);
    //powerTime = 10 + (UpgradesManager.upgradesData.upgrades[UpgradeList.powerTime] * 0.1f);
    GameManager.starfishPowerTime = 10 + EnemyList.specialFish[Fishes.STARFISH][(AquariumManager.aquariumData.fishCards[Fishes.STARFISH].level)];
    GameManager.coinfishMulti = 1 + EnemyList.specialFish[Fishes.COIN][(AquariumManager.aquariumData.fishCards[Fishes.COIN].level)];
    GameManager.treasureMulti = 1 + EnemyList.specialFish[Fishes.TREASURE][(AquariumManager.aquariumData.fishCards[Fishes.TREASURE].level)];
    GameManager.isMultiActive = false;

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
    //boostCoroutine = RefillEnergyTimer();
    isInvincible = false;
    isSpeedBoost = false;
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
    GameObject hat = Instantiate(hats.hatDictionary[hatLoader.GetHat()], hatParent);
    hatRenderer = hat.GetComponent<SpriteRenderer>();
    hatAnim = hat.GetComponent<Animator>();

    //events
    //controlLeft.mobileJoystick.OnMove += MovePlayer;
    //controlRight.mobileJoystick.OnMove += MovePlayer;
    joystick.OnMove += MovePlayer;
    joystick.OnBoostPressed += BoostOn;
    joystick.OnBoostReleased += BoostOff;
    joystick.JumpPlayer += PlayerJump;

    //power setup
    invinsiblePower.SetActive(false);
    multiplyerPower.SetActive(false);
    speedPower.SetActive(false);
    invinsibleBarr.SetActive(false);
    multiBarr.SetActive(false);
    speedBarr.SetActive(false);
    UpdateBarriers();
    //arrow
    arrows = new();

    //sparks
    GameManager.fishEaten += Spark;

    GameManager.isAlive = true;
  }
  private void OnDestroy()
  {
    joystick.OnMove -= MovePlayer;
    joystick.OnBoostPressed -= BoostOn;
    joystick.OnBoostReleased -= BoostOff;
    joystick.JumpPlayer -= PlayerJump;
    GameManager.fishEaten -= Spark;
  }

  private void Spark()
  {
    GameObject go = Instantiate(SparkPrefab, rightWayUp);
    Destroy(go, 1f);
  }

  //Position, boost (UI, Refill, Drain)
  private void Update()
  {
    stats.playerPosition = transform.position;
  }
  #region Movement
  //Move Player
  private void MovePlayer(Vector2 input)
  {
    if (!isJump && !isStun)
    {
      rb2d.velocity = input * (speed + (isSpeedBoost ? boostSpeed : 0));
      angle = Angle(input);
      transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -angle - 90), rotationSpeed * Time.deltaTime);
    }
  }
  //Rotate Player
  public static float Angle(Vector2 vector2)
  {
    if (vector2.x < 0)
      return 360 - (Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg * -1);
    else
      return Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg;
  }

  //called from animation state for right timing
  public void AnimSwim()
  {
    isJump = false;
    if (boostButtonPressed)
    {
      BoostOn();
    }
    else
    {
      BoostOff();
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
    if (!isJump)
    {
      speed = baseSpeed * boostSpeed;
      anim.SetBool("Boost", true);
      isBoost = true;
    }

  }
  private void BoostOff()
  {
    boostButtonPressed = false;
    if (!isJump)
    {
      speed = baseSpeed;
      anim.SetBool("Boost", false);
      isBoost = false;
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
    if (isStun || isJump || !GameManager.isAlive)
      return;
    //Splash routine
    splashSound.Play();
    StartCoroutine(SplashDelay(1f));
    col.isTrigger = true;
    anim.SetTrigger("JumpTrigger");
    hatAnim.SetTrigger("JumpTrigger");

  }
  //called from animation state fro right timing
  public void AnimJump()
  {
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
    splash.GetComponent<Animator>().SetTrigger("Splash");
  }

  IEnumerator SplashDelay(float wait)
  {
    yield return new WaitForSeconds(wait);
    if (!isBoost)
    {
      speed = baseSpeed;
    }
    SplashAnim();
    diveSound.Play();
  }

  private void JumpActions()
  {
    GameManager.dodgeHelper.Clear();
    //slomo
    Time.timeScale = 1f;
    playerSpriteGroup.sortingLayerName = playerSpriteGroup.sortingLayerName == "Jump" ? "Player" : "Jump";
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
    TriggerPower(Random.Range(0, 3));
  }
  public void TriggerPower(int i)
  {
    GameManager.eventText(GameManager.powers[i], 3f);
    switch (i)
    {
      case -1:
        //Extra life
        rb2d.velocity = new();
        PlayerJump();
        extraLife.SetTrigger("Shield");
        if (invisEvent != null) StopCoroutine(invisEvent);
        invisEvent = StartCoroutine(DrainPower(GameManager.starfishPowerTime, invinsiblePower.transform.GetChild(0).GetComponent<Slider>(), invinsiblePower, invinsibleBarr, 0));
        isInvincible = true;
        break;
      case 0:
        //Invincible
        if (isInvincible == true && i != -1) { TriggerPower(Random.Range(0, 3)); return; }
        if (invisEvent != null) StopCoroutine(invisEvent);
        invisEvent = StartCoroutine(DrainPower(GameManager.starfishPowerTime, invinsiblePower.transform.GetChild(0).GetComponent<Slider>(), invinsiblePower, invinsibleBarr, 0));
        isInvincible = true;
        break;
      case 1:
        //Double Points
        if (GameManager.isMultiActive == true) { TriggerPower(Random.Range(0, 3)); return; }
        if (multiEvent != null) StopCoroutine(multiEvent);
        multiEvent = StartCoroutine(DrainPower(GameManager.starfishPowerTime, multiplyerPower.transform.GetChild(0).GetComponent<Slider>(), multiplyerPower, multiBarr, 1));
        GameManager.isMultiActive = true;
        scoreHandler.starfishMultiplyer = 2;
        break;
      case 2:
        //Unlimited Boost
        if (isSpeedBoost == true) { TriggerPower(Random.Range(0, 3)); return; }
        if (speedEvent != null) StopCoroutine(speedEvent);
        speedEvent = StartCoroutine(DrainPower(GameManager.starfishPowerTime, speedPower.transform.GetChild(0).GetComponent<Slider>(), speedPower, speedBarr, 2));
        isSpeedBoost = true;
        break;
    }
  }

  IEnumerator DrainPower(float waitTime, Slider slider, GameObject power, GameObject barr, int i)
  {
    power.SetActive(true);
    barr.SetActive(true);
    UpdateBarriers();
    //IEnumerator image = ImageRotateColors(waitTime, power.transform.GetComponent<Image>());
    //StartCoroutine();
    float timePassed = 0f;
    slider.value = 1;
    while (timePassed < waitTime)
    {
      slider.value = 1 - (timePassed / waitTime);
      timePassed += Time.deltaTime;
      yield return null;
    }
    power.SetActive(false);
    barr.SetActive(false);
    UpdateBarriers();
    switch (i)
    {
      case 0://Invincible
        isInvincible = false;
        break;
      case 1://Double Points
        GameManager.isMultiActive = false;
        scoreHandler.starfishMultiplyer = 1;
        break;
      case 2://Unlimited Boost
        isSpeedBoost = false;
        break;
    }
  }

  void UpdateBarriers()
  {
    int i = 0;
    foreach (Transform barr in multiBarr.transform.parent)
    {
      if (barr.gameObject.activeSelf)
      {
        barr.GetComponent<SpriteRenderer>().sprite = barrs[i];
        i++;
      }
    }
  }

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
  public void Indicator(int val, FishType type, Transform t)
  {
    if (val == 1)
    {
      switch (type)
      {
        case FishType.PREDATOR:
          //red circle indicator
          Instantiate(RedCirclePrefab, RedCircleOrigin);
          //red arrow indicator
          arrows.Add(t, Instantiate(RedArrowPrefab, ArrowOrigin));
          break;
        case FishType.STARFISH:
          arrows.Add(t, Instantiate(StarfishArrowPrefab, ArrowOrigin));
          break;
        case FishType.TREASURE:
          arrows.Add(t, Instantiate(TreasureArrowPrefab, ArrowOrigin));
          break;
      }
      if (arrows.ContainsKey(t))
      {
        arrows[t].GetComponent<IndicatorScript>().PointToward(t, transform);
      }
    }
    else if (val == -1)
    {
      Destroy(arrows[t]);
      arrows.Remove(t);
    }
    EnemyCounter(val);
  }

  public void EnemyCounter(int val)
  {
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
