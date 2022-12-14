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
    public class VendasController : ControllerBase
    {
        private readonly Payment_APIContext  _context;
        public VendasController(Payment_APIContext context)
        {
            _context = context;
        }

        [HttpPost("CriarPedidoVendas")]
        public IActionResult CriarPedidos(Vendas vendas)
        {
            vendas.Status = StatusVendas.Aguadando_Pagamento;
            vendas.Data = DateTime.Now;

            var vendedor = _context.Vendedor.Find(vendas.VendedorId);

            if(vendedor == null)
            {
                return BadRequest(new { Erro = "Vendedor Não Cadastrado!" });
            }

            var produtos = _context.Produtos.Find(vendas.ProdutosId);

            if(produtos == null)
            {
                return BadRequest(new { Erro = "Produto não cadastrado!" });
            }
            
            _context.Add(vendas);
            _context.SaveChanges();
            
            return CreatedAtAction(nameof(VisualizarPedidosPorId), new { id = vendas.Id }, vendas);  
        }

        [HttpGet("VisualizarPedidosPorId")]
        public IActionResult VisualizarPedidosPorId(int id)
        {
            var vendas = _context.Vendas.Find(id);
            var vendedor = _context.Vendedor.Find(vendas.VendedorId);
            var produtos = _context.Produtos.Find(vendas.ProdutosId);

            if(vendas == null){
                return NotFound();
            }

            return Ok(vendas);
        }
        
          [HttpDelete("DeletarVenda")]
        public IActionResult Deletar(int id)
        {
            var vendas = _context.Vendas.Find(id);

            if (vendas == null)
            {
                return NotFound();
            }
            _context.Vendas.Remove(vendas);
            _context.SaveChanges();
            return NoContent();
        }
        
        [HttpPost("AtualizarStatusPedidos")]
        public IActionResult Atualizar(int id, Vendas pedidos)
        {
            var pedidosBd = _context.Vendas.Find(pedidos.Id);
            
            if (pedidosBd.Status == StatusVendas.Aguadando_Pagamento)
            {
                if(pedidos.Status == StatusVendas.Pagamento_Aprovado)
                {
                    pedidosBd.Status = StatusVendas.Pagamento_Aprovado;
                }
                else if(pedidos.Status == StatusVendas.Cancelado)
                {
                    pedidosBd.Status = StatusVendas.Cancelado;
                }
                else
                {
                    return BadRequest(new { Erro = "Atualização não Disponivel!" });    
                }
            }
            else if(pedidosBd.Status == StatusVendas.Pagamento_Aprovado)
            {
                if(pedidos.Status == StatusVendas.Enviado_para_transportadora)
                {
                    pedidosBd.Status = StatusVendas.Enviado_para_transportadora;
                }
                else if(pedidos.Status == StatusVendas.Cancelado)
                {
                    pedidosBd.Status = StatusVendas.Cancelado;
                }
                else
                {
                    return BadRequest(new { Erro = "Atualização não Disponivel!" });    
                }
            }
            else if(pedidosBd.Status == StatusVendas.Enviado_para_transportadora)
            {
                if(pedidos.Status == StatusVendas.Entregue)
                {
                    pedidosBd.Status = StatusVendas.Entregue;
                }
                else
                {
                    return BadRequest(new { Erro = "Atualização não Disponivel!" });
                }
            }
            else
            {
                return BadRequest(new { Erro = "Atualização não Disponivel!" });
            }

            _context.Vendas.Update(pedidosBd);
            _context.SaveChanges();
            return Ok(pedidosBd);
        }

    }
}