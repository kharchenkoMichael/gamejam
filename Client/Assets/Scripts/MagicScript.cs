using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
  public class MagicScript : MonoBehaviour, IPointerClickHandler
  {
    public Action<int> ActionDelegate;
    public int MagicId;

    public void OnPointerClick(PointerEventData eventData)
    {
      if (ActionDelegate != null)
      {
        ActionDelegate(MagicId);
      }
    }
  }
}
