using Model;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameForm : MonoBehaviour
{
  public Action ActionFinishGameDelegate;

  public NewBehaviourScript SignalR;

  public Slider Mana;
  public Slider Hp;

  private void Update()
  {
    if (string.IsNullOrEmpty(SignalR.Name))
      return;

    var user = GameContext.Instance.Users.Find(item => item.Name == SignalR.Name);
    Mana.value = user.Mana;
    Hp.value = user.Hp;

    if (Hp.value <= 0 && ActionFinishGameDelegate != null)
    {
      ActionFinishGameDelegate();
    }
  }
}
