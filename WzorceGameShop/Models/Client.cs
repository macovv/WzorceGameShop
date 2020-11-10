using System.Collections.Generic;

namespace WzorceGameShop.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public decimal Saldo { get; set; }
        public IList<Game> Games { get; set; }
    }
}