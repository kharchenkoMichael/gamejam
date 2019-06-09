﻿using System;
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
      if(HasEffect(User.Posteffects, MagicType.Invisible))
      {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        var effect = User.Posteffects.Where(p => p.Type == MagicType.Invisible).FirstOrDefault();
        User.Posteffects.Remove(effect);
      }

      CreateSpellInstance(dto);
      var posteffect = PosteffectBuilder.GetPosteffect(dto.SpellType);
      User.Posteffects.Add(posteffect);
      CreateSelfEffectObjectInstance(dto);

    }

    public void Attack(MagicType spellType, int damage)
    {
      //todo: если есть защита то проводить действия иначе вычесть жизни
      var effects = User.Posteffects;

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
          var umbrella = User.Posteffects.Where(p => p.Type == MagicType.MagicUmbrella).FirstOrDefault();
          User.Posteffects.Remove(umbrella);
        }
      }

      if (HasEffect(effects, MagicType.MagicMirror))
      {
        //tode : реализовать после создания такого постэффекта
      }

      User.Hp -= damage;
      SignalR.UpdateCapsul(name);
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
            spellObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            var behavior = spellObject.AddComponent<SimpleSpellBehaviorScript>();
            behavior.Target = Enemy;
            spellObject.transform.position = transform.position;
            break;
          }
        case MagicType.Stonefall:
          {
            spellObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var behavior = spellObject.AddComponent<SimpleSpellBehaviorScript>();
            behavior.Target = Enemy;
            //spellObject.transform.position = transform.position;
            spellObject.transform.position = new Vector3(transform.position.x,
                                                          transform.position.y + 10,
                                                          transform.position.z);
            break;
          }
        case MagicType.Fireball:
          {
            spellObject = Instantiate(SignalR.ParticlePrefab);
            var behavior = spellObject.AddComponent<SimpleSpellBehaviorScript>();
            behavior.Target = Enemy;
            spellObject.transform.position = transform.position;
            break;
          }
        case MagicType.IceBolt:
          {
            spellObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            var behavior = spellObject.AddComponent<SimpleSpellBehaviorScript>();
            behavior.Target = Enemy;
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
