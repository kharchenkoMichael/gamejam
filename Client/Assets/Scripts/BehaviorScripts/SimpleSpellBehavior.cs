using Assets.Scripts.Model.MagicFolder;
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

    private int _speed = 1;

    void Update()
    {
      transform.LookAt(Target.transform);
      transform.position += transform.forward * _speed;

      if(Vector3.Distance(transform.position, Target.transform.position) < 5)
      {
        Target.GetComponent<GameBehaviorScript>().Attack(Type, Damage);
        GameObject.Destroy(gameObject);
      }
    }
  }
}
