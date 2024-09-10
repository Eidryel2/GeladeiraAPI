using NewRepository.Models;


namespace Services
{
    public class GeladeiraService
    {
        private GeladeiraContext _context;

        public GeladeiraService(GeladeiraContext context)
        {
            _context = context;
        }

        public List<Iten> ExibirItens()
        {
            return _context.Itens.ToList();
        }

        public void AdicionarItem(int andar, int container, int posicao, Iten item)
        {
            _context.Itens.Add(item);
            _context.SaveChanges();
        }

        public Iten GetItemById(int id)
        {
            return _context.Itens.Find(id);
        }

        public void DeleteItem(int id)
        {
            var item = _context.Itens.Find(id);
            if (item != null)
            {
                _context.Itens.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}
