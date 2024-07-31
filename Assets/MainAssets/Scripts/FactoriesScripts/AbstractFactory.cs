using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AbstractFactory : MonoBehaviour
{

  public int numEnemies;
  public GameObject enemy;
  public PlayerStats player;
  public float sizeOffset;
  private Vector3 pos = Vector3.zero;
  protected Vector3 origin;
  protected GameObject newEnemy;
  protected float r;
  protected float s;
  protected int i;
  protected Identifier identifier;
  protected SortingGroup sortingGroup;

  [Header("Point stats")]
  [SerializeField] protected int value;
  [SerializeField] protected int lower;
  [SerializeField] protected int upper;

  [Space]
  //for special cases, coin fish, starfish, boat, etc.
  protected bool isSpecial = false;
  protected float spawnDelayTime;

  /**
    1. Create empty object
    2. Randomize point within bounds
    3. Populate enemies array
  **/
  void Start()
  {
    r = player.r;
    s = player.s;
    for (i = 0; i < numEnemies; i++)
    {
      AddEnemy();
    }
  }

  protected void AddEnemy()
  {
    UpdateOrigin();
    newEnemy = Instantiate(enemy, SetPosition(), SetRotation(), transform);
    //sortingGroup = newEnemy.AddComponent<SortingGroup>();
    //sortingGroup.sortingLayerName = "Enemy";
    SetPreferences();
  }

  //Sets spawn origin
  protected virtual void UpdateOrigin()
  {
    origin = player.playerPosition;
  }

  protected virtual Quaternion SetRotation()
  {
    return Quaternion.Euler(0, 0, Random.Range(0, 360));
  }

  protected virtual Vector3 SetPosition()
  {
    float a = Random.Range(0f, 1f);
    a = a * 2 * Mathf.PI;
    float b = Random.Range(r, s);
    pos.x = b * Mathf.Cos(a) + origin.x;
    pos.y = b * Mathf.Sin(a) + origin.y;
    return pos;
  }

  //When enemy is eaten or is too far from the player, it is relocated on the map
  public virtual void UpdateObject(Transform o)
  {
    UpdateOrigin();
    o.SetPositionAndRotation(SetPosition(), SetRotation());
  }

  public virtual void SpawnObject(Transform o)
  {
    //UpdateObject(o);
    if (isSpecial)
      StartCoroutine(DelaySpawn(o));

  }

  IEnumerator DelaySpawn(Transform o)
  {
    o.gameObject.SetActive(false);
    yield return new WaitForSeconds(spawnDelayTime);
    UpdateObject(o);
    o.gameObject.SetActive(true);
  }

  //Set unique preferences
  protected virtual void SetPreferences()
  {
  }
  protected void IncreaseNumEnemy()
  {
    if (numEnemies < 10)
    {
      AddEnemy();
      numEnemies++;
    }
  }

}
