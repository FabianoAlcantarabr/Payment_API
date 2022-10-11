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
    public class VendedorController : ControllerBase
   {
        private readonly Payment_APIContext _context;
        public VendedorController(Payment_APIContext context)
        {
            _context = context;
        }

        [HttpPost("CriarVendedor")]
        public IActionResult CriarVendedor(Vendedor vendedor)
        {
            _context.Add(vendedor);
            _context.SaveChanges();
            
            return CreatedAtAction(nameof(VisualizarVendedorPorId), new { id = vendedor.Id }, vendedor);  
        }
        [HttpGet("VisualizarVendedorPorId")]
        public IActionResult VisualizarVendedorPorId(int id)
        {
            var vendedor = _context.Vendedor.Find(id);

            if(vendedor == null){
                return NotFound();
            }

            return Ok(vendedor);
        }
        [HttpGet("VisualizarTodosVendedores")]
        public IActionResult VisualizarVendedores()
        {
            var vendedor = _context.Vendedor;
            return Ok(vendedor);
        }
        [HttpDelete("DeletarVendedor")]
        public IActionResult Deletar(int id)
        {
            var vendedor = _context.Vendedor.Find(id);

            if (vendedor == null){
                return NotFound();
            }
            _context.Vendedor.Remove(vendedor);
            _context.SaveChanges();
            return NoContent();
        }
        [HttpPut("AtualizarVendedor")]
        public IActionResult Atualizar(int id, Vendedor vendedor)
        {
            var vendedorBd = _context.Vendedor.Find(id);
            
            if (vendedorBd == null){
                return NotFound();
            }

            vendedorBd.Nome = vendedor.Nome;
            vendedorBd.Cpf = vendedor.Cpf;
            vendedorBd.Email = vendedor.Email;
            vendedorBd.Telefone = vendedor.Telefone;

            _context.Vendedor.Update(vendedorBd);
            _context.SaveChanges();
            return Ok(vendedorBd);
        }

    }
}