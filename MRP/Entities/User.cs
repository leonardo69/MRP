﻿namespace MRP.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }
    }

    public enum Role {
        Manager,
        Client,
        Admin
    }
}
