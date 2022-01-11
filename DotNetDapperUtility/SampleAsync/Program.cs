using SampleAsync.Models;
using SampleAsync.Services;
using System;
using System.Threading.Tasks;

namespace SampleAsync
{
    class Program
    {
        static async Task Main ()
        {
            await MSSqlTestSingleRecordAsync();
            Console.ReadLine();
        }

        private static async Task MSSqlTestSingleRecordAsync()
        {
            try
            {
                //select - 0 record
                await SingleRecordAsyncService.SelectAllAsync();
                //insert - 1 record
                ProductModel product = await SingleRecordAsyncService.InsertSingleRecordAsync("IPhone");
                //select - 1 record
                await SingleRecordAsyncService.SelectByIdAsync(product);
                //update - 1 record
                product.Name = "Iphone 12";
                product.ModifiedDate = DateTime.Now;
                await SingleRecordAsyncService.UpdateAsync(product);
                //select - 1 record
                await SingleRecordAsyncService.SelectByIdAsync(product);
                //delete - 1 record
                await SingleRecordAsyncService.DeleteAsync(product);
                //select - 0 record
                await SingleRecordAsyncService.SelectByIdAsync(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
