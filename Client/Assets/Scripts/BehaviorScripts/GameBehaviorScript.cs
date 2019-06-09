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
      for (int i = User.Posteffects.Count-1; i >= 0; i--)
      {
        var posteffect = User.Posteffects[i];
        posteffect.Update(User, Time.deltaTime);
      }
    }

    public void Spell(SpellDto dto)
    {
    }

    public void Attack()
    {
      //todo: если есть защита то проводить действия иначе вычесть жизни
      //if(User.Posteffects.Any( e => e.Type == MagicType.))
    }

    private void CreateSpellInstance(SpellDto dto)
    {
      var spellObject = new GameObject();
      switch(dto.SpellType)
      {
        case MagicType.Fireball:
          {
            var behavior = spellObject.AddComponent<SimpleSpellBehaviorScript>();
            behavior.Target = Enemy;
            break;
          }
      }
    }
  }
}
