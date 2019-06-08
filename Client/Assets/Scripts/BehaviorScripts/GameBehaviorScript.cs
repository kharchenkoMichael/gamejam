using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Model.MagicFolder;
using Model.Dto;

namespace Assets.Scripts.BehaviorScripts
{
  class GameBehaviorScript : MonoBehaviour
  {
    public GameObject Enemy;
    public UserDto User;

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
