using System;
using UnityEngine;

public class StartGameScript : MonoBehaviour
{
  public Action ActionDelegate;

  public void StartGame()
  {
    if (ActionDelegate != null)
    {
      ActionDelegate();
    }
  }
}
