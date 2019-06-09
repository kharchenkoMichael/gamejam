using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Model.MagicFolder;

namespace Assets.Scripts
{
  public class MagicScript : MonoBehaviour
  {
    public Action<MagicSprites> ActionDelegate;
    public int MagicId;
    public Sprite sprite;

    void Start()
    {
      var abilityScript = GetComponent<SetAbilities>();
      abilityScript.SetAbility(MagicId-1);
      sprite = abilityScript.GetAbility(MagicId - 1);
    }

    public void ChoosMagic()
    {
      if (ActionDelegate != null)
      {
        ActionDelegate(new MagicSprites
        {
          Id = MagicId,
          Sprite = sprite,
        });
      }
    }
  }
}
