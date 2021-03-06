﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PepegaRequiem.Models
{
    public class Developer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public virtual ICollection<Game> Games { get; set; }
        public Developer()
        {
            Games = new List<Game>();
        }

    }
}