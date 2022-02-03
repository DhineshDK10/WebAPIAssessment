using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDwebapi.Models
{
    public class UserInformation
    {
        [Key]
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
