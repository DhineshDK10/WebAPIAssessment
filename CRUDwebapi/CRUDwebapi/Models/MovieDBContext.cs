using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CRUDwebapi.Models
{
    public class MovieDBContext:DbContext
    {
        public MovieDBContext(DbContextOptions<MovieDBContext> options) : base(options)
        {



        }
        
        public DbSet<UserInformation> userinformation { get; set; }
        public DbSet<MovieDK> movie { get; set; }


    }
}
