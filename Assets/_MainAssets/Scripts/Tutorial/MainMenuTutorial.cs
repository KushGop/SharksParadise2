using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuTutorial : MonoBehaviour
{
  [SerializeField] private ChangeScene cs;
  [SerializeField] private Button button;
  // Start is called before the first frame update
  void Start()
  {
    if (GameManager.playedTutorial)
    {
      button.onClick.AddListener(delegate () { cs.ChangeSceneTo("Game"); });
    }
    else
    {
      button.onClick.AddListener(delegate () { cs.ChangeSceneTo("TutorialGame"); });
    }
  }
}
