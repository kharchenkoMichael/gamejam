using System;
using System.Collections;
using System.Collections.Generic;
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
