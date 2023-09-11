using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{

  /*
  store vector3 of player position, update value in Update()
  store vector3 of center sprite, update in SpriteExit()
  store gameObject[[,,],[,,],[,,]] cointaining all sprites, update order in Reaarange()
  */
  public Transform spriteParent;
  public Transform player;
  private GameObject[] sprites;
  private GameObject[] spritesHold;
  private GameObject centerSprite;
  private float halfDimension;
  private Vector3 centerPosition;
  private float playerX, playerY;
  private GameObject spriteHold;
  private Vector3 positionHold;

  void Start()
  {
    sprites = new GameObject[9];
    spritesHold = new GameObject[9];

    for (int i = 0; i < 9; i++)
    {
      sprites[i] = spriteParent.transform.GetChild(i).gameObject;
    }
    halfDimension = 25f;
    centerSprite = sprites[4];
    centerPosition = centerSprite.transform.position;
    //load background sprites
  }

  void Update()
  {
    playerX = player.position.x;
    playerY = player.position.y;
    if (playerX > centerPosition.x + halfDimension)
    {//right
      Reaarange("right");
    }
    else if (playerX < centerPosition.x - halfDimension)
    {//left
      Reaarange("left");
    }
    else if (playerY > centerPosition.y + halfDimension)
    {//up
      Reaarange("up");
    }
    else if (playerY < centerPosition.y - halfDimension)
    {//down
      Reaarange("down");
    }


    /*
    track player and center sprite,
    if they exit bounds of center sprite,
      update x column or x row sprites depending.
    */
  }
  void Move(int i, int j, bool isX, int multi)
  {
    positionHold = sprites[i].transform.position;
    if (isX)
    {
      positionHold.x += halfDimension * multi;
    }
    else
    {
      positionHold.y += halfDimension * multi;
    }
    sprites[i].transform.position = positionHold;
  }

  void ReaarangeHelp(string direction)
  {
    switch (direction)
    {
      case "up":
        for (int i = 6; i < 15; i++)
        {
          spritesHold[i - 6] = sprites[i % 9];
        }
        break;
      case "down":
        for (int i = 3; i < 12; i++)
        {
          spritesHold[i - 3] = sprites[i % 9];
        }
        break;
      case "right":
        for (int i = 0; i < 9; i+=3)
        {
          spritesHold[i] = sprites[i+1];
          spritesHold[i+1] = sprites[i+2];
          spritesHold[i+2] = sprites[i];
        }
        break;
      case "left":
        for (int i = 0; i < 9; i+=3)
        {
          spritesHold[i] = sprites[i+2];
          spritesHold[i+1] = sprites[i];
          spritesHold[i+2] = sprites[i+1];
        }
        break;
    }
    sprites = (GameObject[])spritesHold.Clone();
  }

  void Reaarange(string direction)
  {
    switch (direction)
    {
      case "up":
        for (int i = 0; i < 3; i++)
        {
          Move(i + 6, i, false, 6);
        }
        break;
      case "down":
        for (int i = 0; i < 3; i++)
        {
          Move(i, i + 6, false, -6);
        }
        break;
      case "right":
        for (int i = 0; i < 9; i += 3)
        {
          Move(i, i + 2, true, 6);
        }
        break;
      case "left":
        for (int i = 0; i < 9; i += 3)
        {
          Move(i + 2, i, true, -6);
        }
        break;
    }
    ReaarangeHelp(direction);
    centerSprite = sprites[4];
    centerPosition = centerSprite.transform.position;
  }

  void PrintItems()
  {
    foreach (GameObject g in sprites)
    {
      Debug.Log(g.name);
    }
  }
}
