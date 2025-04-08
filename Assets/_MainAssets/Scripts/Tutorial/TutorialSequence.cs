using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialSequence : MonoBehaviour
{

  [SerializeField] private GameObject joystick;
  [SerializeField] private GameObject boost;
  [SerializeField] private GameObject jump;
  [SerializeField] private GameObject energyBar;
  [SerializeField] private GameObject fishSpawn;
  [SerializeField] private GameObject birdSpawn;
  [SerializeField] private GameObject score;
  //[SerializeField] private GameObject jellySpawn;
  [SerializeField] private GameObject squidSpawn;
  [SerializeField] private GameObject coinSpawn;
  [SerializeField] private GameObject coinText;
  [SerializeField] private GameObject nightIcon;
  [SerializeField] private GameObject nightCanvas1;
  [SerializeField] private GameObject nightCanvas2;
  [SerializeField] private GameObject bigSpawn;
  [SerializeField] private TextMeshProUGUI tutorialText;
  [SerializeField] private PlayerMovement playerMovement;
  [SerializeField] private TutorialMobileJoystick tutJoystick;
  private readonly float tutorialDelayTime = 4f;

  #region Setup
  void Start()
  {
    TutorialManager.isInTutorial = true;

    TutorialManager.joystick += JoystickSequence;
    TutorialManager.boost += BoostSequence;
    TutorialManager.jump += JumpSequence;
    TutorialManager.energy += EnergySequence;
    TutorialManager.fish += FishSequence;
    TutorialManager.bird += BirdSequence;
    TutorialManager.score += ScoreSequence;
    //TutorialManager.jelly += JellySequence;
    TutorialManager.coinFish += CoinSequence;
    TutorialManager.nightIcon += NightIconSequence;
    TutorialManager.dayNight += NightSequence;
    TutorialManager.bigShark += BigSharkSequence;
    TutorialManager.endSeq += EndSequence;
    TutorialManager.title += TitleScreenComplete;
    TutorialManager.moved += TriggerEvent;
    TutorialManager.boostPressed += TriggerEvent;
    TutorialManager.jumpPressed += TriggerEvent;
    TutorialManager.eatFish += TriggerEvent;
    TutorialManager.eatBird += TriggerEvent;
    TutorialManager.death += TriggerEvent;
    JoystickSequence();
  }
  void OnDestroy()
  {
    TutorialManager.joystick -= JoystickSequence;
    TutorialManager.boost -= BoostSequence;
    TutorialManager.jump -= JumpSequence;
    TutorialManager.energy -= EnergySequence;
    TutorialManager.fish -= FishSequence;
    TutorialManager.bird -= BirdSequence;
    TutorialManager.score -= ScoreSequence;
    //TutorialManager.jelly -= JellySequence;
    TutorialManager.coinFish -= CoinSequence;
    TutorialManager.nightIcon -= NightIconSequence;
    TutorialManager.dayNight -= NightSequence;
    TutorialManager.bigShark -= BigSharkSequence;
    TutorialManager.endSeq -= EndSequence;
    TutorialManager.title -= TitleScreenComplete;
    TutorialManager.isInTutorial = false;
  }

  private void OnDisable()
  {

    TutorialManager.moved -= TriggerEvent;
    TutorialManager.boostPressed -= TriggerEvent;
    TutorialManager.jumpPressed -= TriggerEvent;
    TutorialManager.eatFish -= TriggerEvent;
    TutorialManager.eatBird -= TriggerEvent;
    TutorialManager.death -= TriggerEvent;
  }
  #endregion

  #region StateMachine
  bool trigger = false;
  private void TriggerEvent()
  {
    trigger = true;
  }

  private IEnumerator TextIteratorTwoPart(string[] texts, TutorialManager.TutNextEvent unityEvent, TutorialManager.TutEvent next)
  {
    tutorialText.text = texts[0];
    //yield return new WaitForSeconds(tutorialDelayTime);
    trigger = false;
    yield return new WaitUntil(() => trigger);
    unityEvent -= TriggerEvent;
    tutorialText.text = texts[1];
    yield return new WaitForSeconds(tutorialDelayTime);

    next();
  }

  private IEnumerator TextIteratorOnePart(string[] texts, TutorialManager.TutEvent next)
  {
    for (int i = 0; i < texts.Length; i++)
    {
      tutorialText.text = texts[i];
      yield return new WaitForSeconds(tutorialDelayTime);
    }
    next();
  }
  #endregion

  #region Sequences
  public void StartSequence()
  {
    StartCoroutine(TextIteratorOnePart(new string[] {
    "Welcome to sharks paradise!",
    "Feel free to skip this tutorial at anytime!",
    "Lets get started!"
    },
    TutorialManager.joystick
    ));
  }
  public void JoystickSequence()
  {
    joystick.SetActive(true);
    boost.SetActive(true);
    //playerMovement.DisableEnergy();
    TutorialManager.shouldMove = true;
    StartCoroutine(TextIteratorTwoPart(new string[] {
    "Use the joystick to swim around!",
    "Use this to chase or run away from other fish!"
    },
    TutorialManager.moved,
    TutorialManager.fish
    ));
  }
  public void BoostSequence()
  {
    StartCoroutine(TextIteratorTwoPart(new string[] {
    "Drag the joystick further to boost!",
    "Use boost to catch smaller fish or swim away from bigger sharks!"
    },
    TutorialManager.boostPressed,
    TutorialManager.fish
    ));
  }
  public void FishSequence()
  {
    fishSpawn.SetActive(true);
    StartCoroutine(TextIteratorTwoPart(new string[] {
    "Let's try catching some fish!",
    "Nice!"
    },
    TutorialManager.eatFish,
    TutorialManager.jump
    ));
  }
  public void JumpSequence()
  {
    jump.SetActive(true);
    tutJoystick.SetJumpDistance();
    StartCoroutine(TextIteratorTwoPart(new string[] {
    "Drag the joystick even more to jump!",
    "You can use this to catch birds or jump over bigger sharks!"
    },
    TutorialManager.jumpPressed,
    TutorialManager.bird
    ));
  }
  public void BirdSequence()
  {
    birdSpawn.SetActive(true);
    StartCoroutine(TextIteratorTwoPart(new string[] {
    "Now let's try catching some birds",
    "Nice!"
    },
    TutorialManager.eatBird,
    TutorialManager.score
    ));
  }
  public void ScoreSequence()
  {
    score.SetActive(true);
    StartCoroutine(TextIteratorOnePart(new string[] {
    "When you catch fish your score will go higher!",
    "Catching the same fish will increase your multiplyer!"
    },
    TutorialManager.energy
    ));
  }

  public void EnergySequence()
  {
    //playerMovement.EnableEnergy();
    //energyBar.SetActive(true);
    StartCoroutine(TextIteratorOnePart(new string[] {
    "Using boost or jump will drain your energy",
    "You can catch fish to fill it up"
    },
    TutorialManager.coinFish
    ));
  }
  //public void JellySequence()
  //{
  //  jellySpawn.SetActive(true);
  //  StartCoroutine(TextIteratorOnePart(new string[] {
  //  "Careful! Some fish are more defensive than others!",
  //  "Higher risk gives higher scores!"
  //  },
  //  TutorialManager.coinFish
  //  ));
  //}
  public void CoinSequence()
  {
    coinSpawn.SetActive(true);
    coinText.SetActive(true);
    StartCoroutine(TextIteratorOnePart(new string[] {
    "Look out for rare fish!",
    "Chasing them can help! but try not to get too distracted!"
    },
    TutorialManager.nightIcon
    ));
  }
  public void NightIconSequence()
  {
    nightIcon.SetActive(true);
    TutorialManager.night();
    StartCoroutine(TextIteratorOnePart(new string[] {
    "Notice the icon in the top left corner.",
    "This will show you how much day and night time you have left!"
    },
    TutorialManager.dayNight
    ));
  }
  public void NightSequence()
  {
    GameManager.switchToNight();
    StartCoroutine(TextIteratorOnePart(new string[] {
    "Visibility is lowered at night so be careful!",
    "This indicator on your back will help you out."
    },
    TutorialManager.bigShark
    ));
  }
  public void BigSharkSequence()
  {
    GameManager.switchToDay();
    bigSpawn.SetActive(true);
    StartCoroutine(TextIteratorOnePart(new string[] {
    "Your back will turn red when there is danger near!",
    "Bigger sharks will try to chase you.",
    "If they catch you, game over."
    },
    TutorialManager.endSeq
    ));
  }

  public void SkipSequence()
  {
    StopAllCoroutines();
    StartCoroutine(TextIteratorOnePart(new string[] {
    "It seems like you're ready to go out for a swim!",
    "You can come back to the tutorial anytime",
    "Remember to have fun in..."
    },
    TutorialManager.title
    ));
  }

  public void EndSequence()
  {
    StartCoroutine(TextIteratorOnePart(new string[] {
    "It seems like you're ready to go out for a swim!",
    "Remember to have fun in..."
    },
    TutorialManager.title
    ));
  }

  public void TitleScreenComplete()
  {
    GameManager.playedTutorial = true;
    DataPersistenceManager.instance.SaveGame();
    SceneManager.LoadScene("MainMenu");
  }
  public void TitleScreenIncomplete()
  {
    SceneManager.LoadScene("MainMenu");
  }

  #endregion
}
