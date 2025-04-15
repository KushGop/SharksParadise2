using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PadColor : MonoBehaviour
{
  [SerializeField] Image[] images;
  [SerializeField] Color opaque;
  [SerializeField] Color tranlucent;
  [SerializeField] Color transparent;

  int back, forward;

  private void Start()
  {
    foreach (Image i in images)
      i.color = tranlucent;
  }

  internal void SetColours(int index)
  {
    if (index < 0)
    {
      if (index == -4) index = 4;
      if (index == -3) index = 5;
      if (index == -2) index = 6;
      if (index == -1) index = 7;
    }
    back = ((index + 8) - 1) % 8;
    forward = ((index + 8) + 1) % 8;

    for (int i = 0; i < 8; i++)
    {
      if (i == back || i == forward)
        images[i].color = tranlucent;
      else if (i == index)
        images[i].color = opaque;
      else
        images[i].color = transparent;
    }
  }
}
