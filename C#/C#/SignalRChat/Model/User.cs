using System.Collections.Generic;

namespace SignalRChat.Model
{
    public class User
    {
        public string Name { get; }
        public int Hp { get; set; }
        public Position Position { get; set; }
        public List<Magic> Magic { get; set; }
        public int RoomName { get; }
        //public bool isDead { get; set; }

        public User(string name, Position position, int roomNumber)
        {
            Name = name;
            Position = position;
            RoomName = roomNumber;
        }

        public void Clone(User user)
        {
            this.Position = user.Position;
        }
    }
}