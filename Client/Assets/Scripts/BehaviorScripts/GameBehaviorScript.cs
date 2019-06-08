using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Model.MagicFolder;

namespace Assets.Scripts.BehaviorScripts
{
  class GameBehaviorScript : MonoBehaviour
  {
    public GameObject Enemy;

    void Start()
    {
      
    }

    void Update()
    {
      
    }

    public void Spell(SpellDto dto)
    {
      var ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
      var behavior = ball.AddComponent<SimpleSpellBehaviorScript>();
      behavior.Target = Enemy;
    }

  }
}
