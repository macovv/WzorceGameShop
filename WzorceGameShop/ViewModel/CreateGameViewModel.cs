using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WzorceGameShop.Data;
using WzorceGameShop.Models;

namespace WzorceGameShop.ViewModel
{
    public class CreateGameViewModel
    {
        public Game Game { get; set; }
        public SelectList Category { get; set; } 
        public int IdCategory { get; set; }
        public SelectList Studio { get; set; }
        public int IdStudio { get; set; }


        public CreateGameViewModel(Game game, List<Category> categories, List<Studio> studios) 
        {
            this.Game = game;
           
            this.Category = new SelectList(categories, "Id", "Name");

            this.Studio = new SelectList(studios, "Id", "Name");
        }

        public CreateGameViewModel() { } 

    }

}


