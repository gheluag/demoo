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
        MySqlConnection connection = new MySqlConnection("server=localhost;user=root;password=123456789;port=3306;database=AllForLife");


        public List<Products> GetProducts()
        {

            List<Products> productsList = new List<Products>();

            string query = "select product.*, nameCategory, " +
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
                    products.CategoryId = Convert.ToInt32(reader["categoryId"]);
                    products.BrandId = Convert.ToInt32(reader["brandId"]);
                    products.SupplierId = Convert.ToInt32(reader["supplierId"]);
                    products.Count = Convert.ToInt32(reader["count"]);

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

            string query = "select select product.*, nameCategory, " +
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
                    products.CategoryId = Convert.ToInt32(reader["categoryId"]);
                    products.BrandId = Convert.ToInt32(reader["brandId"]);
                    products.SupplierId = Convert.ToInt32(reader["supplierId"]);
                    products.Count = Convert.ToInt32(reader["count"]);

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

            string query = "select select product.*, nameCategory, " +
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
                    products.CategoryId = Convert.ToInt32(reader["categoryId"]);
                    products.BrandId = Convert.ToInt32(reader["brandId"]);
                    products.SupplierId = Convert.ToInt32(reader["supplierId"]);
                    products.Count = Convert.ToInt32(reader["count"]);

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



        public List<Categories> GetCategories()
        {
            List<Categories> catlst = new();

            string query = "select * from category";


            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Categories cat = new();
                    cat.Id = Convert.ToInt32(reader["idCategory"]);
                    cat.Name = reader["nameCategory"].ToString();

                    catlst.Add(cat);
                   
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


            return catlst;
        }


        public List<Suppliers> GetSuppliers()
        {
            List<Suppliers> suplst = new();

            string query = "select * from supplier";


            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Suppliers sup = new();
                    sup.Id = Convert.ToInt32(reader["idSupplier"]);
                    sup.Name = reader["nameSupplier"].ToString();

                    suplst.Add(sup);

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


            return suplst;
        }



        public List<Brand> GetBrands()
        {
            List<Brand> brandlst = new();

            string query = "select * from brand";


            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    Brand brand = new();
                    brand.Id = Convert.ToInt32(reader["idBrand"]);
                    brand.Name = reader["nameBrand"].ToString();

                    brandlst.Add(brand);

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


            return brandlst;
        }


        public bool UpdateProduct(Products product)
        {
            string query = @"UPDATE product SET 
                        productName = @name,
                        article = @article,
                        price = @price,
                        currentSale = @currentSale,
                        maxSale = @maxSale,
                        prodDesc = @desc,
                        imageURL = @img,
                        categoryId = @catId,
                        supplierId = @supId,
                        brandId = @brandId
                     WHERE idProduct = @id";

            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@article", product.Article);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@currentSale", product.CurrentSale);
                cmd.Parameters.AddWithValue("@maxSale", product.MaxSale);
                cmd.Parameters.AddWithValue("@desc", product.Description);
                cmd.Parameters.AddWithValue("@img", product.ImageURL ?? ""); 
                cmd.Parameters.AddWithValue("@catId", product.CategoryId);
                cmd.Parameters.AddWithValue("@supId", product.SupplierId);
                cmd.Parameters.AddWithValue("@brandId", product.BrandId);
                cmd.Parameters.AddWithValue("@id", product.Id);

                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }


    }
}
