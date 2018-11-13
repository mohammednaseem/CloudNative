using System;
using System.IO;
using System.Threading.Tasks;
using DHLM.Common.DataAccess.Repository;

namespace DHLM.Common.DataAccess.AzureSql
{
    public class AzureSql : ILogRepository
    {
        public string StorageAccount { get; }
        
        public AzureSql(string storageAccount)
        {
            StorageAccount = storageAccount;
        }
       
        public string WriteTelegram(string textContents)
        {
            string message = null;
            try{
            	//implement logic
                Console.WriteLine("Telegram written from DHLM.Common.DataAccess.AzureStorage.TelgramLogRepository");
            }
            catch(Exception ex)
            {//temporary
                Console.WriteLine("Exception Occured" + ex);
                throw;
            }        
            return message;
        }      
    }
}