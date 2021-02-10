using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PepegaRequiem.Models
{
   
    public class Game
    {
        public int GameId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int? DeveloperID { get; set; }
        public decimal? Price { get; set; }
        [Required]
        public int? CategoryID { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public Developer Developer { get; set; }
    }
    
}