using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDwebapi.Models
{
    public class MovieDK
    {
        [Key]
        public int Id { get; set; }
        public string MovieName { get; set; }
        public string Descriptions { get; set; }
        public string Movie_Type { get; set; }
        public string Languages { get; set; }

    }
}
