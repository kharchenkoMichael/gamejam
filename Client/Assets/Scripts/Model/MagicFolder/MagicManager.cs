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
      _magic.Add(1, new Magic("Молния", MagicType.Lightning, "Повреждает противника ударом сверху (укрытия не защищают) управления игрока меняется случайным образом"));
      _magic.Add(2, new Magic("Камнепад", MagicType.Stonefall, "Повреждает противника ударом сверху (укрытия не защищают), игрок оглушается на некоторое время."));
      _magic.Add(3, new Magic("Фаербол", MagicType.Fireball, "Повреждает противника, но поджигает игрока, нанося ему периодический урон. Летит по прямой, можно спрятаться за укрытия."));
      _magic.Add(4, new Magic("Ледяной болт", MagicType.IceBolt, "Наносит противнику урон, но руки игрока замерзают и он некоторое время не может кастовать никакие заклинания. Летит по прямой, можно спрятаться за укрытия."));

      _magic.Add(5, new Magic("Превратиться в воду", MagicType.TurnIntoWater, "Не получает повреждения от камнепада, фаербола, но получает двойное повреждение от молнии и ледяного болта."));
      _magic.Add(6, new Magic("Стать невидимым", MagicType.Invisible, "Все атакующие заклинания с ударом сверху не работают. Если заклинания которое летит по прямой попадает в цель, оно срабатывает и невидимость пропадает. Игрок не знает где он."));
      _magic.Add(7, new Magic("Волшебный зонтик", MagicType.MagicUmbrella, "Блокирует заклинания которые летят по прямой, есть шанс что откроется с задержкой от 1-5 секунд."));
      _magic.Add(8, new Magic("Зеркало мага", MagicType.MagicMirror, " Есть шанс что заклинания вернется на атакующего с вероятностью 30%, есть шанс что заклинания наносит двойной урон с вероятностью 70%."));

      _magic.Add(9, new Magic("Ловушка с шипами", MagicType.Trap, "Рандомно срабатывающая ловушка."));
      _magic.Add(10, new Magic("Торнадо", MagicType.Tornado, "Вихрь, который блуждает по локации и наносит повреждения всему, к чему дотронется."));
      _magic.Add(11, new Magic("Зыбучие пески", MagicType.Quicksand, "Скорость всех в области уменьшена в два раза."));
      _magic.Add(12, new Magic("Облачность", MagicType.CloudCover, "Локацию закрывают тучи и игроки не видят происходящего за ними."));
    }

    public List<Magic> GetAllMagic()
    {
      return _magic.Values.ToList();
    }

    public List<Magic> GetAllAvailable()
    {
      return _magic.Select(x => x.Value).Where(x => !x.isChoosen).ToList();
    }
    
  }
}
