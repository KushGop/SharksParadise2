using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollection : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    Resources.UnloadUnusedAssets();
    System.GC.Collect();
  }
}
