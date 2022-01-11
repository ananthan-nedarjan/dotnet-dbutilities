using DotNetDapperUtilityLibrary.MSSql;
using SampleAsync.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAsync.Services
{
    public class SingleRecordAsyncService
    {
        public static readonly string MSSQL_CONNECTION_STRING = ConfigurationManager.ConnectionStrings["SQL_CONNECTION_STRING"].ToString();

        public static async Task SelectAllAsync()
        {
            try
            {
                string query = "select * from [SalesLT].[ProductModel]";

                using (MSSqlService _sql = new MSSqlService(MSSQL_CONNECTION_STRING))
                {
                    List<ProductModel> products = await _sql.QueryListAsync<ProductModel>(query, isStoredProc: false);

                    Console.WriteLine($"SelectAll => Selected Records:{products.Count}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<ProductModel> InsertSingleRecordAsync(string productName)
        {
            try
            {
                string insertQuery = "insert into [SalesLT].[ProductModel] values (@Name, @ModifiedDate)";
                string selectQuery = "select * from [SalesLT].[ProductModel] where Name = @Name";
                ProductModel product = new ProductModel { Name = productName, ModifiedDate = DateTime.Now };

                ProductModel insertedProduct = null;
                using (MSSqlService _sql = new MSSqlService(MSSQL_CONNECTION_STRING))
                {
                    bool isInserted = await _sql.ExecuteAsync(insertQuery, product, false);

                    Console.WriteLine($"InsertSingleRecord => isInserted:{isInserted}");

                    if (isInserted)
                    {
                        insertedProduct = await _sql.QueryFirstOrDefaultAsync<ProductModel>(selectQuery, product, false);
                    }

                    return insertedProduct;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<ProductModel> SelectByIdAsync(ProductModel product)
        {
            try
            {
                string query = "select * from [SalesLT].[ProductModel] where ProductModelID = @ProductModelID";

                using (MSSqlService _sql = new MSSqlService(MSSQL_CONNECTION_STRING))
                {
                    ProductModel selectedProduct = await _sql.QueryFirstOrDefaultAsync<ProductModel>(query, product, isStoredProc: false);

                    if (selectedProduct != null)
                    {
                        Console.WriteLine($"SelectById => Product Id:{selectedProduct.ProductModelID}");
                        Console.WriteLine($"SelectById => Product Name:{selectedProduct.Name}");
                        Console.WriteLine($"SelectById => Modified Date:{selectedProduct.ModifiedDate}");
                    }
                    else
                    {
                        Console.WriteLine($"SelectById => SelectedProduct: null");
                    }
                    return selectedProduct;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task UpdateAsync(ProductModel model)
        {
            try
            {
                string query = "update [SalesLT].[ProductModel] set Name = @Name, ModifiedDate = @ModifiedDate where ProductModelID = @ProductModelID";

                using (MSSqlService _sql = new MSSqlService(MSSQL_CONNECTION_STRING))
                {
                    bool isUpdated = await _sql.ExecuteAsync(query, model, isStoredProc: false);

                    Console.WriteLine($"Update => isUpdated:{isUpdated}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task DeleteAsync(ProductModel model)
        {
            try
            {
                string query = "Delete [SalesLT].[ProductModel] where ProductModelID = @ProductModelID";

                using (MSSqlService _sql = new MSSqlService(MSSQL_CONNECTION_STRING))
                {
                    bool isDeleted = await _sql.ExecuteAsync(query, model, isStoredProc: false);

                    Console.WriteLine($"Delete => isDeleted:{isDeleted}");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
