using Microsoft.AspNetCore.Mvc;
using GELADEIRA;
using Repository;
using System.ComponentModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GeladeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeladeiraController : ControllerBase
    {

        private Geladeira _geladeira;
        private readonly List<Item> _itens = new List<Item>();

        IConfiguration _configuration;
        ItensRepository _repository;
        public List<Item> Itens { get; set; }

        public GeladeiraController(IConfiguration configuration)
        {

            _configuration = configuration;
            _repository = new ItensRepository(_configuration);


            _geladeira = new Geladeira();
            Item novoItem = new("Pera", 3);
            Item novoItem2 = new("laranja", 5);
            Item novoItem3 = new("uva", 4);
            _geladeira.AdicionarItem(0, 1, 1, novoItem);
            Console.WriteLine($"Item adicionado: {novoItem.Id}");
            _geladeira.AdicionarItem(0, 1, 2, novoItem2);
            _geladeira.AdicionarItem(0, 1, 0, novoItem3);

        }

        // GET:get - retorna todos os itens - FUNCIONANDO
        [HttpGet]
        public IActionResult Get()
        {
            var itens = _repository.RetornarItens();
            return Ok(itens);
        }

        // GET ID - FUNCIONANDO
        [HttpGet("{id}")]
        public IActionResult GetItemById(int id)
        {
            var itens = _geladeira.Itens;
            var item = itens.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                Console.WriteLine($"Item não encontrado com ID {id}");
                return NotFound();
            }
            Console.WriteLine($"Item encontrado com ID {id}");

            return Ok(item);
        }
        private void AtualizarItens()
        {
            Itens = _geladeira.Itens.ToList();
        }
        // POST FUNCIONANDO
        [HttpPost]
        public IActionResult AdicionarItem(int andar, int container, int posicao, [FromBody] Item item)
        {
            try
            {
                if (andar < 0 || andar >= _geladeira.Andares.Count)
                    return BadRequest("AndarInvalido");

                if (container < 0 || container >= _geladeira.Andares[andar].Containers.Count)
                    return BadRequest("ContainerInvalido");

                if (posicao < 0 || posicao >= _geladeira.Andares[andar].Containers[container].Posicoes.Count)
                    return BadRequest("PosicaoInvalida");


                _geladeira.AdicionarItem(andar, container, posicao, item);
                _repository.CriarItem(item);
                return Ok(_repository.RetornarItens());
          
    
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar item: {ex.Message}");
                return BadRequest("Erro ao adicionar item");
            }
        }

        // PUT api/<GeladeiraController>/5
       

        // DELETE FUINCIONANDO
        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            try
            {
                _repository.DeletarItem(id);
                return Ok("Item deletado");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao deletar item");
            }
        }
    }
 }

