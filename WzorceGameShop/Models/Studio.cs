﻿using System.Collections.Generic;

namespace WzorceGameShop.Models
{
    public class Studio
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Game> Games { get; set; }
    }
}