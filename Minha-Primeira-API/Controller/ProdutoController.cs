using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minha_Primeira_API.Model;
using System;

namespace Minha_Primeira_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        // Rota para obter todos os produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            List<Produto> produtos;
            // Consulta o banco de dados para obter a lista de produtos
            produtos = await _context.Produtos.ToListAsync();

            if (produtos == null || produtos.Count == 0)
            {
                return NotFound(); // Retorna 404 Not Found se nenhum produto for encontrado
            }

            return produtos; // Retorna a lista de produtos
        }
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return Created("GetProduto", produto);

        }

        [HttpPut("{id}")]
        public async  Task<IActionResult> Put(int id, [FromBody] Produto produtoAtualizado)
        {
            Produto produtoExistente = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if (produtoExistente == null)
            {
                return NotFound(); // Retorna um código 404 caso o produto não seja encontrado.
            }

            // Atualiza as propriedades do produto existente com as novas propriedades.
            produtoExistente.NomeProduto = produtoAtualizado.NomeProduto;
            produtoExistente.ValorProduto = produtoAtualizado.ValorProduto;

            await _context.SaveChangesAsync();

            return NoContent(); // Retorna um código 204 indicando que a operação foi bem-sucedida, mas não há conteúdo para retornar.
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var produtoExistente = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if (produtoExistente == null)
            {
                return NotFound(); // Retorna um código 404 caso o produto não seja encontrado.
            }

            _context.Produtos.Remove(produtoExistente); // Remove o produto da lista (ou do banco de dados, se aplicável).

            await _context.SaveChangesAsync();

            return NoContent(); // Retorna um código 204 indicando que a operação foi bem-sucedida, mas não há conteúdo para retornar.
        }
    }
}
