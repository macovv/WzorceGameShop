using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WzorceGameShop.Models
{
    public class BillGame
    {
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int BillId { get; set; }
        public Bill Bill { get; set; }
    }
}
