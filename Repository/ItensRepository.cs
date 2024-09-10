using GELADEIRA;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Text;
using static GELADEIRA.Geladeira;
namespace Repository
{
    public class ItensRepository
    {
        IConfiguration _configuration;
        string connectionString;

        public ItensRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public List<Item> RetornarItens()
        {
            var listItens = new List<Item>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    var query = new StringBuilder();
                    query.AppendLine("SELECT Id, Nome, Andar, Container, Posicao");
                    query.AppendLine("FROM Itens");

                    //command
                    SqlCommand cmd = new SqlCommand(query.ToString(), connection);
                    cmd.CommandType = CommandType.Text;

                    //abre a conexao
                    connection.Open();

                    //executa a consulta e adiciona o resultado em um stream
                    SqlDataReader itensDataReader = cmd.ExecuteReader();

                    while (itensDataReader.Read())
                    {
                        var item = new Item(
                     itensDataReader["Nome"].ToString(),
                     Convert.ToInt32(itensDataReader["Id"])

                 );

                        listItens.Add(item);
                    }
                }

                return listItens;
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao retornar itens", ex);
            }
        }
        public bool CriarItem(Item item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    var query = new StringBuilder();
                    query.AppendLine("INSERT INTO Itens (Nome, Id)");
                    query.AppendLine("VALUES (@Nome, @Id)");


                    //command
                    SqlCommand cmd = new SqlCommand(query.ToString(), connection);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Nome", item.Nome);
                    cmd.Parameters.AddWithValue("@Id", item.Id);

                    //abre a conexao
                    connection.Open();

                    //executa a consulta
                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao criar item", ex);
            }
        }

        public bool AtualizarItem(Item item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    var query = new StringBuilder();
                    query.AppendLine("UPDATE Itens");
                    query.AppendLine("SET Nome = @Nome");
                    query.AppendLine("WHERE Id = @Id");

                    //command
                    SqlCommand cmd = new SqlCommand(query.ToString(), connection);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Nome", item.Nome);

                    //abre a conexao
                    connection.Open();

                    //executa a consulta
                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao atualizar item", ex);
            }
        }

        public bool DeletarItem(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    var query = new StringBuilder();
                    query.AppendLine("DELETE FROM Itens");
                    query.AppendLine("WHERE Id = @Id");

                    //command
                    SqlCommand cmd = new SqlCommand(query.ToString(), connection);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Id", id);

                    //abre a conexao
                    connection.Open();

                    //executa a consulta
                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Erro ao deletar item", ex);
            }
        }
    }
}