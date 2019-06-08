using SignalRChat.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat.Model
{
    public class Room
    {
        public string Name { get; }
        public bool isActive;
        private List<UserDto> Users { get; }

        public Room(string name)
        {
            Name = name;
            isActive = false;
            Users = new List<UserDto>();
        }

        public List<UserDto> GetAllUsers()
        {
            return Users;
        }
        public void AddUser(UserDto user)
        {
            Users.Add(user);
        }
    }
}