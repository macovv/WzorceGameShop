using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WzorceGameShop.Models
{
    public class ClientGame
    {
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
