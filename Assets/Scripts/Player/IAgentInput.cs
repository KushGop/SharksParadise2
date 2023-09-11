using System;
using UnityEngine;

public interface IAgentInput
{
  event Action<Vector2> OnMovement;
  event Action OnBoostReleased;
  event Action OnBoostPressed;
  event Action JumpPlayer;
}