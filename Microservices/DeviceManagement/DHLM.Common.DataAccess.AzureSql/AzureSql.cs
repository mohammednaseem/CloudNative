using System;
using System.IO;
using System.Threading.Tasks;
using DHLM.Common.DataAccess.Repository;

namespace DHLM.Common.DataAccess.AzureSql
{
    public class AzureSql : ILogRepository
    {
        public string SqlConnectionString { get; }
        
        public AzureSql(string sqlConnectionString)
        {
            SqlConnectionString = sqlConnectionString;
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