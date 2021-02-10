using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PepegaRequiem.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

    }
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}