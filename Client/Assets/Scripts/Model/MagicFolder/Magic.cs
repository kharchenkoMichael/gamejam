using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.MagicFolder
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
