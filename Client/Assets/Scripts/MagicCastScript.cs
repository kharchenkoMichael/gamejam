using Model.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCastScript : MonoBehaviour
{
  public Action ActionDelegate;

  public void Attack()
  {
    if (ActionDelegate != null)
    {
      ActionDelegate();
    }
  }
}
