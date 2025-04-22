using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace AllForLife.Entity
{
    public class DataBase
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;user=root;password=1234;port=3306;database=AllForLife");


        public List<Products> GetProducts()
        {

            List<Products> productsList = new List<Products>();

            string query = "select idProduct, productName, article, price, " +
                "maxSale, currentSale, count, prodDesc, imageURL, nameCategory, " +
                "nameBrand, nameSupplier " +
                "from product " +
                "join category on categoryId = idCategory " +
                "join supplier on supplierId = idSupplier " +
                "join brand on brandId = idBrand";

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Products products = new Products();

                    products.Id = Convert.ToInt32(reader["idProduct"]);
                    products.Name = reader["productName"].ToString();
                    products.Article = reader["article"].ToString();
                    products.Price = Convert.ToDecimal(reader["price"]);
                    products.MaxSale = Convert.ToInt32(reader["maxSale"]);
                    products.CurrentSale = Convert.ToInt32(reader["currentSale"]);
                    products.Description = reader["prodDesc"].ToString();
                    products.ImageURL = reader["imageURL"].ToString();
                    products.Category = reader["nameCategory"].ToString();
                    products.Supplier = reader["nameSupplier"].ToString();
                    products.Brand = reader["nameBrand"].ToString();

                    productsList.Add(products);
                }

                reader.Close();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

            return productsList;
        }

        public List<Products> GetSaleProducts(int minSale, int maxSale)
        {

            List<Products> productsList = new List<Products>();

            string query = "select idProduct, productName, article, price, " +
                "maxSale, currentSale, count, prodDesc, imageURL, nameCategory, " +
                "nameBrand, nameSupplier " +
                "from product " +
                "join category on categoryId = idCategory " +
                "join supplier on supplierId = idSupplier " +
                "join brand on brandId = idBrand " +
                "where currentSale between @minSale and @maxSale";

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@minSale", minSale);
                command.Parameters.AddWithValue("@maxSale", maxSale);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Products products = new Products();

                    products.Id = Convert.ToInt32(reader["idProduct"]);
                    products.Name = reader["productName"].ToString();
                    products.Article = reader["article"].ToString();
                    products.Price = Convert.ToDecimal(reader["price"]);
                    products.MaxSale = Convert.ToInt32(reader["maxSale"]);
                    products.CurrentSale = Convert.ToInt32(reader["currentSale"]);
                    products.Description = reader["prodDesc"].ToString();
                    products.ImageURL = reader["imageURL"].ToString();
                    products.Category = reader["nameCategory"].ToString();
                    products.Supplier = reader["nameSupplier"].ToString();
                    products.Brand = reader["nameBrand"].ToString();

                    productsList.Add(products);
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

            return productsList;
        }


        public List<Products> SearchResults(string seacrhText)
        {
            List<Products> productslst = new();

            string query = "select idProduct, productName, article, price, " +
                "maxSale, currentSale, count, prodDesc, imageURL, nameCategory, " +
                "nameBrand, nameSupplier " +
                "from product " +
                "join category on categoryId = idCategory " +
                "join supplier on supplierId = idSupplier " +
                "join brand on brandId = idBrand " +
                "where productName like @searchTxt";


            try
            {
                connection.Open();


                MySqlCommand command = new(query,  connection);
                command.Parameters.AddWithValue("@searchTxt", "%" + seacrhText + "%");

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Products products = new Products();

                    products.Id = Convert.ToInt32(reader["idProduct"]);
                    products.Name = reader["productName"].ToString();
                    products.Article = reader["article"].ToString();
                    products.Price = Convert.ToDecimal(reader["price"]);
                    products.MaxSale = Convert.ToInt32(reader["maxSale"]);
                    products.CurrentSale = Convert.ToInt32(reader["currentSale"]);
                    products.Description = reader["prodDesc"].ToString();
                    products.ImageURL = reader["imageURL"].ToString();
                    products.Category = reader["nameCategory"].ToString();
                    products.Supplier = reader["nameSupplier"].ToString();
                    products.Brand = reader["nameBrand"].ToString();

                    productslst.Add(products);
                }

                reader.Close();


            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

            productslst = productslst.Distinct().ToList();
            return productslst;

        }


        public Users AuthenticateUser(string uName, string passw)
        {
            Users users = null;

            string query = "select u.*, roleName " +
                "from users u " +
                "join roles on u.roleId = idRole " +
                "where username = @uname and passw = @passw";

            try
            {
                connection.Open();

                MySqlCommand cmd = new(query, connection);

                cmd.Parameters.AddWithValue("@uname", uName);
                cmd.Parameters.AddWithValue("@passw", passw);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users = new Users
                    {
                        Id = Convert.ToInt32(reader["idUser"]),
                        FirstName = reader["firstname"].ToString(),
                        LastName = reader["lastname"].ToString(),
                        MiddleName = reader["patronymic"].ToString(),
                        Username = reader["username"].ToString(),
                        Password = reader["passw"].ToString(),
                        Role = reader["roleName"].ToString()
                    };
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }

            return users;
        }



        
    }
}
