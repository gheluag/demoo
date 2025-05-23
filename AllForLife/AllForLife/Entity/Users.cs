﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllForLife.Entity
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? MiddleName { get; set; }
        public string Password { get; set; }

        public string FullName => $"{LastName} {FirstName} {MiddleName}";

        public string Role {  get; set; }
    }
}
