﻿using Assets.Scripts.Model.MagicFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.BehaviorScripts
{
  class SimpleSpellBehaviorScript : MonoBehaviour
  {
    public MagicType Type;
    public GameObject Target;
    public int Damage;
    //public int _LIfeTime = -1; // -1 : до паражения цели; больше 0 : время жизни в секундах (для молнии)  

    private float _speed = 0.2f;

    void Update()
    {
      transform.LookAt(Target.transform);
      transform.position += transform.forward * _speed;

      if(Vector3.Distance(transform.position, Target.transform.position) < 0.6)
      {
        var behaviorScript = Target.GetComponent<GameBehaviorScript>();
        behaviorScript.Attack(Type, Damage);
        Destroy(gameObject);
      }
    }
  }
}
