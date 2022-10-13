using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payment_API.Context;
using Payment_API.Models;

namespace Payment_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
   {
         private readonly Payment_APIContext _context;

         public ProdutosController(Payment_APIContext context)
        {
            _context = context;
        }

        [HttpPost("CriarProdutos")]
        public IActionResult CriarProdutos(Produtos produtos)
        {
            _context.Add(produtos);
            _context.SaveChanges();
            
            return CreatedAtAction(nameof(VisualizarProdutosPorId),
             new { id = produtos.Id }, produtos);  
        }

        [HttpGet("VisualizarProdutosPorId")]
        public IActionResult VisualizarProdutosPorId(int id)
        {
            var produtos = _context.Produtos.Find(id);

            if(produtos == null)
            {
                return NotFound();
            }

            return Ok(produtos);
        }

        [HttpGet("VisualizarTodosProdutos")]
        public IActionResult VisualizarProdutos()
        {
            var produtos = _context.Produtos;
            return Ok(produtos);
        }

        [HttpDelete("DeletarProdutos")]
        public IActionResult Deletar(int id)
        {
            var produtos = _context.Produtos.Find(id);

            if (produtos == null)
            {
                return NotFound();
            }
            _context.Produtos.Remove(produtos);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("AtualizarValorProdutos")]
        public IActionResult Atualizar(int id, Produtos item)
        {
            var produtosBd = _context.Produtos.Find(id);
            
            if (produtosBd == null){
                return NotFound();
            }
                 

            _context.Produtos.Update(produtosBd);
            _context.SaveChanges();
            return Ok(produtosBd);
        }
    
    }
}