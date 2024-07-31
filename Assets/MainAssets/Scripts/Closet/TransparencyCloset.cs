using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransparencyCloset : MonoBehaviour
{
  private Color colorRef;
  Image image;
  float distance;
  // Start is called before the first frame update
  void Start()
  {
    image = transform.GetComponent<Image>();
    distance = 500;
    colorRef = Color.white;
    colorRef.a = 1;
  }

  // Update is called once per frame
  void Update()
  {
    //print(Mathf.Cos(Mathf.Abs((transform.position.x - 1170) / distance)));
    colorRef.a = Mathf.Cos(Mathf.Abs((transform.position.x - 1170) / distance));
    image.color = colorRef;
  }
}
