using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicContainerScript : MonoBehaviour {

  public Dictionary<int, GameObject> Elements;
  public void InitializeMagic () {

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
