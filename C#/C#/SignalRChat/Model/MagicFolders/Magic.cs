using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat.Model.MagicFolders
{
    public class Magic
    {
        public MagicType Type;
        public string Name;
        public int Damage;

        public Magic(string name, MagicType type)
        {
            Name = name;
            Type = type;
        }
    }
}