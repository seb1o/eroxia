using eroxia.model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eroxia
{
    internal class DBStorage : IStorage
    {
        public static string postgresConnectionString = "Host=localhost;Port=5432;Database=eroxia;Username=postgres;Password=superpippo;";

        public async Task<List<Product>> GetProductsFromDB()
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(postgresConnectionString);
            using var dataSource = dataSourceBuilder.Build();
            using var connection = await dataSource.OpenConnectionAsync();
            using var query = new Npgsql.NpgsqlCommand("SELECT id_product, name, manufacturer, price, material, color FROM product", connection);

            using var reader = query.ExecuteReader();

            var products = new List<Product>();
            while (reader.Read())
            {
                var product = new Product(
                    reader.GetInt32(0), // IdProduct
                    reader.GetString(1), // Name
                    reader.GetString(2), // Manufacturer
                    reader.GetDecimal(3) // Price
                )
                {
                    Material = reader.IsDBNull(4) ? null : reader.GetString(4), // Material
                    Color = reader.IsDBNull(5) ? null : reader.GetString(5) // Color
                };
                products.Add(product);

            }

            //reader.Close();
            //connection.Close();
            return products;
        }

        public async Task<List<Employee>> GetEmployeesFromDB()
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(postgresConnectionString);
            using var dataSource = dataSourceBuilder.Build();
            using var connection = await dataSource.OpenConnectionAsync();
            using var query = new Npgsql.NpgsqlCommand("SELECT fiscal_code, name, surname, dob FROM employee", connection);

            using var reader = query.ExecuteReader();

            var employees = new List<Employee>();
            while (reader.Read())
            {
                var employee = new Employee(
                    reader.GetString(0), // FiscalCode
                    reader.GetString(1), // Name
                    reader.GetString(2), // Surname
                    reader.GetDateTime(3) // Dob
                );
                employees.Add(employee);

            }
            return employees;
        }

        public async Task<int> InsertProductToDB(Product product)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(postgresConnectionString);
            using var dataSource = dataSourceBuilder.Build();
            using var connection = dataSource.OpenConnection();
            var queryString = "INSERT INTO product (name, manufacturer, price, material, color) VALUES (@name, @manufacturer, @price, @material, @color) RETURNING id_product";
            using var query = new Npgsql.NpgsqlCommand(queryString, connection);

            query.Parameters.AddWithValue("name", product.Name);
            query.Parameters.AddWithValue("manufacturer", product.Manufacturer);
            query.Parameters.AddWithValue("price", product.Price);
            query.Parameters.AddWithValue("material", product.Material ?? (object)DBNull.Value);
            query.Parameters.AddWithValue("color", product.Color ?? (object)DBNull.Value);

            //query.ExecuteNonQuery(); //mi dice quante righe inserite

            object? resultId = null;

            try
            {
                resultId = await query.ExecuteScalarAsync();
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine($"Error inserting product: {ex.Message}");
                throw;
            }


            if (resultId != null && int.TryParse(resultId.ToString(), out int idProduct)) //string perchè ExecuteScalarAsync() (la prima colonna della prima riga) non per forza è id int
            {
                return idProduct;
            }
            else
            {
                throw new Exception("Failed to insert product into the database.");
            }
        }

        public async Task<bool> DeleteProductFromDB(int idProduct)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(postgresConnectionString);
            using var dataSource = dataSourceBuilder.Build();
            using var connection = dataSource.OpenConnection();

            //BEGIN si usa per transaction, quando c'è errore ritorna e recupera le cose eliminate
            //delete anche purchase_product perchè c'è constraint, il prodotto non posso eliminarlo se è presente in purchase_product
            var queryString = @"BEGIN;
                               DELETE FROM purchase_product WHERE id_product = @idProduct;
                               DELETE FROM product WHERE id_product = @idProduct;
                               COMMIT;
                              ";
            //ROLLBACK si usa con SAVAPOINT, quando c'è errore ritorna al savepointe recupera le cose eliminate
            //abbiamo poche query quindi basta un BEGIN e COMMIT, di default(in teoria) il savepoint è BEGIN

            //nelle query mai usare inerpolazioni

            using var query = new Npgsql.NpgsqlCommand(queryString, connection);

            query.Parameters.AddWithValue("idProduct", idProduct); //nelle query mai usare interpolazioni

            var result = await query.ExecuteNonQueryAsync();//esegui qualcosa che non restituisce un reader
            //restituisce int, 0 non ha trovato, -1 errore, altro numero la riga trovata/colpita e delete eseguito

            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<List<Client>> GetClientsFromDB()
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(postgresConnectionString);
            using var dataSource = dataSourceBuilder.Build();
            using var connection = await dataSource.OpenConnectionAsync();
            using var query = new Npgsql.NpgsqlCommand("SELECT fiscal_code, name, surname, address, fiscal_code_employee FROM client", connection);

            using var reader = query.ExecuteReader();

            var clients = new List<Client>();
            while (reader.Read())
            {
                var client = new Client(
                    reader.GetString(0), // FiscalCode
                    reader.GetString(1), // Name
                    reader.GetString(2), // Surname
                    reader.GetString(3) // Address
                )
                {
                    FiscalCodeEmployee = reader.IsDBNull(4) ? null : reader.GetString(4)
                };
                clients.Add(client);

            }
            return clients;
        }
    }
}