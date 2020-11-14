using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WzorceGameShop.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public IList<Game> Games { get; set; }
        public decimal SummaryPrice { get; set; }
        public Client Client { get; set; }
    }
}
