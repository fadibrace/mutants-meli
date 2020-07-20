using System.Threading.Tasks;

namespace MutantsAPI.Utils
{
    public interface IHousekeeperJob
    {
        Task LoadReadDbJob();
    }
}