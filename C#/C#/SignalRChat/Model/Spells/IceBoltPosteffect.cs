﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignalRChat.Model.Dto;
using SignalRChat.Model.MagicFolders;

namespace Assets.Scripts.Model.Spells
{
  public class IceBoltPosteffect : ISpellPosteffect
  {
    public MagicType Type { get { return MagicType.IceBolt; } }
    public bool isActive { get; set; }

    //private GameObject _firstSpellButton;
    //private GameObject _secondSpellButton;
    //private GameObject[] _spellButtons;
    private double _timer;

    public void Start(UserDto user)
    {
      isActive = true;

      /*_spellButtons = GameObject.FindGameObjectsWithTag("SpellButton");
      foreach (var button in _spellButtons)
        button.SetActive(false);*/
    }

    public void Update(UserDto user, double deltaTimeSec)
    {
      /*_timer -= deltaTimeSec;
      if(_timer <= 0)
        foreach (var button in _spellButtons)
          button.SetActive(true);*/
    }
  }
}