using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PepegaRequiem.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        [Required]
        public int GameId { get; set; }
        public DateTime DateTime { get; set; }
        public string User { get; set; }
        public string Email { get; set; }
     

    }
}