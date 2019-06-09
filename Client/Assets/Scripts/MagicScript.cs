using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
  public class MagicScript : MonoBehaviour
  {
    public Action<int> ActionDelegate;
    public int MagicId;

    void Start()
    {
      GetComponent<SetAbilities>().SetAbility(MagicId-1);
    }

    public void ChoosMagic()
    {
      if (ActionDelegate != null)
      {
        ActionDelegate(MagicId);
      }
    }
  }
}
