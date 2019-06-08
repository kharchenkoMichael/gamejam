using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.MagicFolder
{
  public class MagicManager
  {
    private Dictionary<int, Magic> _magic;
    public MagicManager()
    {
      _magic = new Dictionary<int, Magic>();
      _magic.Add(1, new Magic(1, "Молния", MagicType.Attack, ""));
      _magic.Add(2, new Magic(2, "", MagicType.Attack, ""));
      _magic.Add(3, new Magic(3, "", MagicType.Attack, ""));
      _magic.Add(4, new Magic(4, "", MagicType.Attack, ""));

      _magic.Add(5, new Magic(5, "", MagicType.Defence, ""));
      _magic.Add(6, new Magic(6, "", MagicType.Defence, ""));
      _magic.Add(7, new Magic(7, "", MagicType.Defence, ""));
      _magic.Add(8, new Magic(8, "", MagicType.Defence, ""));

      _magic.Add(9, new Magic(9, "", MagicType.Effect, ""));
      _magic.Add(10, new Magic(10, "", MagicType.Effect, ""));
      _magic.Add(11, new Magic(11, "", MagicType.Effect, ""));
      _magic.Add(12, new Magic(12, "", MagicType.Effect, ""));
    }

    public List<Magic> GetAllMagic()
    {
      return _magic.Values.ToList();
    }

    public List<Magic> GetAllAvailable()
    {
      return _magic.Select(x => x.Value).Where(x => !x.isChoosen).ToList();
    }

    public void UpdateMagic(Magic updated)
    {
      _magic[updated.Id] = updated;
    }
  }
}
