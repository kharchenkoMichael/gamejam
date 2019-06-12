using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebornScript : MonoBehaviour
{
  public Action ActionDelegate;

  public void Reborn()
  {
    if (ActionDelegate != null)
    {
      ActionDelegate();
    }
  }
}
