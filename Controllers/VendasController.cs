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
    public class VendasControllers : ControllerBase
    {
        private readonly Payment_APIContext  _context;
        public VendasControllers(Payment_APIContext context)
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
                return BadRequest(new { Erro = "Vendedor Não Cadastrado" });
            }

            var item = _context.Produtos.Find(vendas.ItemId);

            if(item == null)
            {
                return BadRequest(new { Erro = "Item não cadastrado!" });
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
            var produtos = _context.Produtos.Find(vendas.ItemId);

            if(vendas == null){
                return NotFound();
            }

            return Ok(vendas);
        }
        
        [HttpPost("AtualizarStatusPedidos")]
        public IActionResult Atualizar(int id, Vendas pedidos)
        {
            var pedidoBd = _context.Vendas.Find(pedidos.Id);
            
            if (pedidoBd.Status == StatusVendas.Aguadando_Pagamento)
            {
                if(pedidos.Status == StatusVendas.Pagamento_Aprovado)
                {
                    pedidoBd.Status = StatusVendas.Pagamento_Aprovado;
                }
                else if(pedidos.Status == StatusVendas.Cancelado)
                {
                    pedidoBd.Status = StatusVendas.Cancelado;
                }
                else
                {
                    return BadRequest(new { Erro = "Atualização não disponivel!" });    
                }
            }
            else if(pedidoBd.Status == StatusVendas.Pagamento_Aprovado)
            {
                if(pedidos.Status == StatusVendas.Enviado_para_transportadora)
                {
                    pedidoBd.Status = StatusVendas.Enviado_para_transportadora;
                }
                else if(pedidos.Status == StatusVendas.Cancelado)
                {
                    pedidoBd.Status = StatusVendas.Cancelado;
                }
                else
                {
                    return BadRequest(new { Erro = "Atualização não disponivel!" });    
                }
            }
            else if(pedidoBd.Status == StatusVendas.Enviado_para_transportadora)
            {
                if(pedidos.Status == StatusVendas.Entregue)
                {
                    pedidoBd.Status = StatusVendas.Entregue;
                }
                else
                {
                    return BadRequest(new { Erro = "Atualização não disponivel!" });
                }
            }
            else
            {
                return BadRequest(new { Erro = "Atualização não disponivel!" });
            }

            _context.Vendas.Update(pedidoBd);
            _context.SaveChanges();
            return Ok(pedidoBd);
        }
        

    }
}