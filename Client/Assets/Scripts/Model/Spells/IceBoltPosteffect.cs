﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Model.MagicFolder;
using Model.Dto;
using UnityEngine;

namespace Assets.Scripts.Model.Spells
{
  public class IceBoltPosteffect : ISpellPosteffect
  {
    public MagicType Type { get { return MagicType.IceBolt; } }


    //private GameObject _firstSpellButton;
    //private GameObject _secondSpellButton;
    private GameObject[] _spellButtons;
    private double _timer;

    public void Start(UserDto user)
    {
      _spellButtons = GameObject.FindGameObjectsWithTag("SpellButton");
      foreach (var button in _spellButtons)
        button.SetActive(false);
    }

    public void Update(UserDto user, double deltaTimeSec)
    {
      _timer -= deltaTimeSec;
      if(_timer <= 0)
        foreach (var button in _spellButtons)
          button.SetActive(true);
    }
  }
}