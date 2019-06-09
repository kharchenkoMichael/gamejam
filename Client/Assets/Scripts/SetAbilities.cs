using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetAbilities : MonoBehaviour
{
  public Sprite[] Sprites;

  public void SetAbility(int sprite)
  {
    if (sprite < 0 || sprite >= Sprites.Length)
      return;

    gameObject.GetComponent<Image>().sprite = Sprites[sprite];
  }
}
