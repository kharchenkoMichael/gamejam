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
    public GameObject Target;

    private int _speed = 1;

    void Update()
    {
      transform.LookAt(Target.transform);
      transform.position += transform.forward * _speed;
    }
  }
}
