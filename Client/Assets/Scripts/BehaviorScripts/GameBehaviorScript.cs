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
      var user = GameContext.Instance.Users.FirstOrDefault(item => item.Name == UserName);
      if (user == null)
        return;
      
      for (int i = user.Posteffects.Count - 1; i >= 0; i--)
      {
        var posteffect = user.Posteffects[i];
        posteffect.Update(user, Time.deltaTime);
      }
      
      SignalR.UpdateCapsul(user);
    }
     
    public void Spell(SpellDto dto)
    {
      var user = GameContext.Instance.Users.Find(item => item.Name == UserName);
      if(HasEffect(user.Posteffects, MagicType.Invisible))
      {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        var effect = user.Posteffects.Where(p => p.Type == MagicType.Invisible).FirstOrDefault();
        user.Posteffects.Remove(effect);
      }

      CreateSpellInstance(dto);
      var posteffect = PosteffectBuilder.GetPosteffect(dto.SpellType);
      user.Posteffects.Add(posteffect);
      //CreateSelfEffectObjectInstance(dto);

    }

    public void Attack(MagicType spellType, int damage)
    {
      var user = GameContext.Instance.Users.FirstOrDefault(item => item.Name == UserName);
      if (user == null)
        return;
      
      //todo: если есть защита то проводить действия иначе вычесть жизни
      var effects = user.Posteffects;

      if (HasEffect(effects, MagicType.TurnIntoWater))
      {
        if (spellType == MagicType.Lightning || spellType == MagicType.IceBolt)
          damage *= 2;
      }

      if (HasEffect(effects, MagicType.Invisible))
      {
        if (spellType == MagicType.Lightning || spellType == MagicType.Stonefall)
          damage = 0;
      }

      if (HasEffect(effects, MagicType.MagicUmbrella))
      {
        if (spellType == MagicType.Fireball || spellType == MagicType.IceBolt)
        {
          damage = 0;
          var umbrella = user.Posteffects.Where(p => p.Type == MagicType.MagicUmbrella).FirstOrDefault();
          user.Posteffects.Remove(umbrella);
        }
      }

      if (HasEffect(effects, MagicType.MagicMirror))
      {
        //tode : реализовать после создания такого постэффекта
      }

      user.Hp -= damage;
      SignalR.UpdateCapsul(user);
    }

    private bool HasEffect(List<ISpellPosteffect> effects, MagicType type)
    {
      return effects.Any(e => e.Type == type && e.isActive);
    }


    private void CreateSpellInstance(SpellDto dto)
    {
      GameObject spellObject;
      switch (dto.SpellType)
      {
        case MagicType.Lightning:
          {
            spellObject = Instantiate(SignalR.ParticlePrefab[(int) MagicType.Lightning]);
            var behavior = spellObject.AddComponent<SimpleSpellBehaviorScript>();
            behavior.Target = Enemy;
            behavior.Damage = 15;
            spellObject.transform.position = transform.position;
            break;
          }
        case MagicType.Stonefall:
          {
            spellObject = Instantiate(SignalR.ParticlePrefab[(int) MagicType.Stonefall]);
            var behavior = spellObject.AddComponent<SimpleSpellBehaviorScript>();
            behavior.Target = Enemy;
            behavior.Damage = 20;
            //spellObject.transform.position = transform.position;
            spellObject.transform.position = new Vector3(transform.position.x,
                                                          transform.position.y + 10,
                                                          transform.position.z);
            break;
          }
        case MagicType.Fireball:
          {
            spellObject = Instantiate(SignalR.ParticlePrefab[(int) MagicType.Fireball]);
            var behavior = spellObject.AddComponent<SimpleSpellBehaviorScript>();
            behavior.Target = Enemy;
            behavior.Damage = 10;
            spellObject.transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
            break;
          }
        case MagicType.IceBolt:
          {
            spellObject = Instantiate(SignalR.ParticlePrefab[(int) MagicType.IceBolt]);
            var behavior = spellObject.AddComponent<SimpleSpellBehaviorScript>();
            behavior.Target = Enemy;
            behavior.Damage = 10;
            spellObject.transform.position = transform.position;
            spellObject.transform.position = new Vector3(spellObject.transform.position.x, spellObject.transform.position.y + 2, spellObject.transform.position.z);
            break;
          }
      }
    }

    private void CreateSelfEffectObjectInstance(SpellDto dto)
    {
      GameObject selfEffectObject;
      switch (dto.SpellType)
      {
        case MagicType.Lightning:
          {
            selfEffectObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            selfEffectObject.transform.position = transform.position;
            selfEffectObject.transform.SetParent(transform);
            break;
          }
        case MagicType.Stonefall:
          {
            selfEffectObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            selfEffectObject.transform.position = transform.position;
            selfEffectObject.transform.SetParent(transform);
            break;
          }
        case MagicType.Fireball:
          {
            selfEffectObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            selfEffectObject.transform.position = transform.position;
            selfEffectObject.transform.SetParent(transform);
            break;
          }
        case MagicType.IceBolt:
          {
            selfEffectObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            selfEffectObject.transform.position = transform.position;
            selfEffectObject.transform.SetParent(transform);
            break;
          }
        case MagicType.TurnIntoWater:
          {
            //turn into water
            break;
          }
        case MagicType.Invisible:
          {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            break;
          }

      }
    }

  }
}
