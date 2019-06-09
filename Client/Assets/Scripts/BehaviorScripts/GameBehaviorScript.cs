using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Model.MagicFolder;
using Model.Dto;
using Assets.Scripts.Model.Spells;
using Model;

namespace Assets.Scripts.BehaviorScripts
{
  class GameBehaviorScript : MonoBehaviour
  {
    public GameObject Enemy;
    public string UserName;
    public NewBehaviourScript SignalR;

    void Start()
    {
    }

    void Update()
    {
      var user = GameContext.Instance.Users.Find(item => item.Name == UserName);
      for (int i = user.Posteffects.Count - 1; i >= 0; i--)
      {
        var posteffect = user.Posteffects[i];
        posteffect.Update(user, Time.deltaTime);
      }
    }

    public void Spell(SpellDto dto)
    {
      CreateSpellInstance(dto);
    }

    public void Attack(MagicType spellType, int damage)
    {
      var user = GameContext.Instance.Users.Find(item => item.Name == UserName);
      //todo: если есть защита то проводить действия иначе вычесть жизни
      var effects = user.Posteffects;

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

      user.Hp -= damage;
      SignalR.UpdateCapsul(name);
    }

    private bool HasEffect(List<ISpellPosteffect> effects, MagicType type)
    {
      return effects.Any(e => e.Type == type && e.isActive);
    }


    private void CreateSpellInstance(SpellDto dto)
    {
      var spellObject = Instantiate(SignalR.ParticlePrefab);
      switch (dto.SpellType)
      {
        case MagicType.Fireball:
          {
            var behavior = spellObject.AddComponent<SimpleSpellBehaviorScript>();
            behavior.Damage = 10;
            behavior.Target = Enemy;
            spellObject.transform.position = transform.position;
            spellObject.transform.position = new Vector3(spellObject.transform.position.x, spellObject.transform.position.y + 2, spellObject.transform.position.z);
            break;
          }
      }
    }
  }
}
