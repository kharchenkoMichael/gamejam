using Model;
using System;
using System.Linq;
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

    var user = GameContext.Instance.Users.FirstOrDefault(item => item.Name == SignalR.Name);
    if (user == null)
      return;
    
    Mana.value = user.Mana;
    Hp.value = user.Hp;

    if (Hp.value <= 0 && ActionFinishGameDelegate != null)
    {
      ActionFinishGameDelegate();
    }
  }
}
