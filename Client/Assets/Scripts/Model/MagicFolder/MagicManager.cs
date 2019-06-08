using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Model.MagicFolder
{
  public class MagicManager
  {
    private Dictionary<MagicType, Magic> _magic;
    public MagicManager()
    {
      _magic = new Dictionary<MagicType, Magic>();
      _magic.Add(MagicType.Attack, new Magic("Молния", MagicType.Attack, "Повреждает противника ударом сверху (укрытия не защищают) управления игрока меняется случайным образом"));
      _magic.Add(MagicType.Attack, new Magic("Камнепад", MagicType.Attack, "Повреждает противника ударом сверху (укрытия не защищают), игрок оглушается на некоторое время."));
      _magic.Add(MagicType.Attack, new Magic("Фаербол", MagicType.Attack, "Повреждает противника, но поджигает игрока, нанося ему периодический урон. Летит по прямой, можно спрятаться за укрытия."));
      _magic.Add(MagicType.Attack, new Magic("Ледяной болт", MagicType.Attack, "Наносит противнику урон, но руки игрока замерзают и он некоторое время не может кастовать никакие заклинания. Летит по прямой, можно спрятаться за укрытия."));

      _magic.Add(MagicType.Defence, new Magic("Превратиться в воду", MagicType.Defence, "Не получает повреждения от камнепада, фаербола, но получает двойное повреждение от молнии и ледяного болта."));
      _magic.Add(MagicType.Defence, new Magic("Стать невидимым", MagicType.Defence, "Все атакующие заклинания с ударом сверху не работают. Если заклинания которое летит по прямой попадает в цель, оно срабатывает и невидимость пропадает. Игрок не знает где он."));
      _magic.Add(MagicType.Defence, new Magic("Волшебный зонтик", MagicType.Defence, "Блокирует заклинания которые летят по прямой, есть шанс что откроется с задержкой от 1-5 секунд."));
      _magic.Add(MagicType.Defence, new Magic("Зеркало мага", MagicType.Defence, " Есть шанс что заклинания вернется на атакующего с вероятностью 30%, есть шанс что заклинания наносит двойной урон с вероятностью 70%."));

      _magic.Add(MagicType.Effect, new Magic("Ловушка с шипами", MagicType.Effect, "Рандомно срабатывающая ловушка."));
      _magic.Add(MagicType.Effect, new Magic("Торнадо", MagicType.Effect, "Вихрь, который блуждает по локации и наносит повреждения всему, к чему дотронется."));
      _magic.Add(MagicType.Effect, new Magic("Зыбучие пески", MagicType.Effect, "Скорость всех в области уменьшена в два раза."));
      _magic.Add(MagicType.Effect, new Magic("Облачность", MagicType.Effect, "Локацию закрывают тучи и игроки не видят происходящего за ними."));
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
