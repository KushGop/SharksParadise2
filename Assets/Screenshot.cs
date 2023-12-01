using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
  int i;
  void Start()
  {
    i = 0;
  }
  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.S))
    {
      Debug.Log("screenshot");
      ScreenCapture.CaptureScreenshot("SharksParadise_Screenshot" + i + ".png");
      i++;
    }
  }
}
