using Assets.Scripts;
using Assets.Scripts.Model.MagicFolder;
using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.UI;

public class MagicContainerScript : MonoBehaviour
{
  private MagicManager _magicManager;
  public Dictionary<int, GameObject> Elements;
  
  public void InitializeMagic ()
  {
    _magicManager = new MagicManager();
    Elements = new Dictionary<int, GameObject>();
    foreach (var item in gameObject.GetComponentsInChildren<Button>())
    {
      if (item != null)
      {
        var script = item.gameObject.GetComponent<MagicScript>();
        Elements.Add(script.MagicId, item.gameObject);
      }
    }
  }
	
}
