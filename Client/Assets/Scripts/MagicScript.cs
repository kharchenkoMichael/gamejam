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

    public void ChoosMagic()
    {
      if (ActionDelegate != null)
      {
        ActionDelegate(MagicId);
      }
    }
  }
}
