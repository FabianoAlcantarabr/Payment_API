using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payment_API.Models
{
     public class Vendas
    {
        [Key()]
        public int Id { get; set; }
        [ForeignKey("Vendedor")]
        public int VendedorId { get; set; }
        public virtual Vendedor Vendedor { get; set; }
        [ForeignKey("Produtos")]
        public int ProdutosId { get; set; }
        public virtual Produtos Produtos { get; set; }
        public DateTime Data { get; set; }
        public StatusVendas Status { get; set; }
    }
}