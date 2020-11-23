using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WzorceGameShop.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public Studio Studio { get; set; }
        public int? StudioId { get; set; }
        public decimal Price { get; set; }
        public bool Promotion { get; set; }
        public string Description { get; set; }
        public int PG { get; set; }
        public ICollection<BasketGame> BasketGames { get; set; }
    }
}
