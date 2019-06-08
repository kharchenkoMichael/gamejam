using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat.Model.Dto
{
    public class RoomUpdateDto
    {
        public Dictionary<int, Room> Rooms;
    }
}