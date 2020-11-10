using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WzorceGameShop.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public IList<Game> SelectedGames { get; set; }
    }
}
