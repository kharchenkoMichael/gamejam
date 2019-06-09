using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Model.MagicFolder;
using Model.Dto;
using Assets.Scripts.Model.Spells;

namespace Assets.Scripts.BehaviorScripts
{
  class GameBehaviorScript : MonoBehaviour
  {
    public GameObject Enemy;
    public UserDto User;
    public NewBehaviourScript SignalR;

    void Start()
    {

    }

    void Update()
    {
      for (int i = User.Posteffects.Count - 1; i >= 0; i--)
      {
        var posteffect = User.Posteffects[i];
        posteffect.Update(User, Time.deltaTime);
      }
    }

    public void Spell(SpellDto dto)
    {
      CreateSpellInstance(dto);
    }

    public void Attack(MagicType spellType, int damage)
    {
      //todo: если есть защита то проводить действия иначе вычесть жизни
      var effects = User.Posteffects;

      if (HasEffect(effects,MagicType.TurnIntoWater))
      {
        if (spellType == MagicType.Lightning || spellType == MagicType.IceBolt)
          damage *= 2;
      }

      if(HasEffect(effects, MagicType.Invisible))
      {
        if (spellType == MagicType.Lightning || spellType == MagicType.Stonefall)
          damage = 0;
      }

      if (HasEffect(effects, MagicType.MagicUmbrella))
      {
        if (spellType == MagicType.Fireball || spellType == MagicType.IceBolt)
          damage = 0;
      }

      if (HasEffect(effects, MagicType.MagicMirror))
      {
        //tode : реализовать после создания такого постэффекта
      }

      User.Hp -= damage;
      SignalR.UpdateCapsul(transform);
    }

    private bool HasEffect(List<ISpellPosteffect> effects, MagicType type)
    {
      return effects.Any(e => e.Type == type && e.isActive);
    }


    private void CreateSpellInstance(SpellDto dto)
    {
      var spellObject = new GameObject();
      switch (dto.SpellType)
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
