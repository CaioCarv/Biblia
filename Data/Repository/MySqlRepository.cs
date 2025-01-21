using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Biblia.Data.Repository
{
    public class MySqlRepository
    {
        private readonly string _connectionString;

        public MySqlRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IEnumerable<string> GetDadosBiblia()
        {
            var resultados = new List<string>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Biblia";

                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            resultados.Add($"{reader["ID"]} - {reader["LIVROS"]}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return resultados;
        }
    }
}
