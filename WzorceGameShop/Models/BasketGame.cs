using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WzorceGameShop.Models
{
    public class BasketGame
    {
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
