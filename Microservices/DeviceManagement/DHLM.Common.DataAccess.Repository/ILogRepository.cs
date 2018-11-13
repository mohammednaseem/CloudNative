using System.Threading.Tasks;

namespace DHLM.Common.DataAccess.Repository
{
    public interface ILogRepository
    {
        string WriteTelegram(string message);
    }
}