using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat.Model
{
  public class Position
  {
    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public float PositionZ { get; set; }

    public Position(float x, float y, float z)
    {
      PositionX = x;
      PositionY = y;
      PositionZ = z;
    }
  }
}