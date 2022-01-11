using Sample.Models;
using Sample.Services;
using System;

namespace Sample
{
    class Program
    {
        
        static void Main()
        {
            MSSqlTestSingleRecord();
            Console.ReadLine();
        }

        private static void MSSqlTestSingleRecord()
        {
            try
            {
                //select - 0 record
                SingleRecordService.SelectAll();
                //insert - 1 record
                ProductModel product = SingleRecordService.InsertSingleRecord("IPhone");
                //select - 1 record
                SingleRecordService.SelectById(product);
                //update - 1 record
                product.Name = "Iphone 12";
                product.ModifiedDate = DateTime.Now;
                SingleRecordService.Update(product);
                //select - 1 record
                SingleRecordService.SelectById(product);
                //delete - 1 record
                SingleRecordService.Delete(product);
                //select - 0 record
                SingleRecordService.SelectById(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
