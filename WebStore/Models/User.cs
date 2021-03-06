﻿using System.Collections.Generic;

namespace WebStore.Models
{
    public class User
    {
        public int ID { get; set; }
        public bool Admin { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AddressOne { get; set; }
        public string AddressTwo { get; set; }

        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
    }
}