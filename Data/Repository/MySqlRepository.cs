using MySql.Data.MySqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Biblia.Models;

namespace Biblia.Data.Repository
{
    public class MySqlRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<MySqlRepository> _logger;

        public MySqlRepository(string connectionString, ILogger<MySqlRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        /// <summary>
        /// Retorna todos os livros da Bíblia
        /// </summary>
        public List<LivroModel> GetDadosBiblia()
        {
            var resultados = new List<LivroModel>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT ID, Nome FROM livros";

                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultados.Add(new LivroModel
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Nome = reader["Nome"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao buscar livros.");
                }
            }
            return resultados;
        }

        /// <summary>
        /// Retorna informações de um livro pelo ID
        /// </summary>
        public LivroModel BuscarLivroPorId(int id)
        {
            LivroModel livro = null;
            using (var connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT ID, Nome, Resumo FROM livros WHERE ID = @id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            livro = new LivroModel
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Nome = reader["Nome"].ToString(),
                                Resumo = reader["Resumo"] != DBNull.Value ? reader["Resumo"].ToString() : "Resumo não disponível"
                            };
                        }
                    }
                }
            }
            return livro;
        }

        /// <summary>
        /// Retorna o número total de capítulos de um livro pelo ID
        /// </summary>
        public int BuscarTotalCapitulos(int idLivro)
        {
            int totalCapitulos = 0;
            using (var connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM capitulos WHERE idlivro = @idLivro";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idLivro", idLivro);
                    connection.Open();
                    totalCapitulos = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return totalCapitulos;
        }

        // Método para sortear um livro aleatório
        public LivroModel SortearLivro()
        {
            LivroModel livro = null;
            using (var connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT ID, Nome FROM livros ORDER BY RAND() LIMIT 1";
                using (var command = new MySqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            livro = new LivroModel
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Nome = reader["Nome"].ToString()
                            };
                        }
                    }
                }
            }
            return livro;
        }

        // Método para obter o total de capítulos de um livro
        public int ObterTotalCapitulos(int livroId)
        {
            int totalCapitulos = 0;
            using (var connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM capitulos WHERE idlivro = @livroId";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@livroId", livroId);
                    connection.Open();
                    totalCapitulos = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            return totalCapitulos;
        }


    }
}
