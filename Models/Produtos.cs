using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Payment_API.Models
{
     public class Produtos
    {
        [Key()]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
    }
}