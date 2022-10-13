using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payment_API.Models;

namespace Payment_API.Context
{
    public class Payment_APIContext : DbContext
   {
      public Payment_APIContext(DbContextOptions<Payment_APIContext> options) : base(options)
      {

      } 

      public DbSet<Produtos> Produtos {get; set; }
      public DbSet<Vendas> Vendas {get; set; }
      public DbSet<Vendedor> Vendedor {get; set; }
 
        
    }
}