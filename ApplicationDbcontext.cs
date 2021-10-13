using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebpApiAutores.Models;

namespace WebpApiAutores
{
    public class ApplicationDbcontext : DbContext
    {

        public ApplicationDbcontext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Autor> autores { get; set; }

    }
}