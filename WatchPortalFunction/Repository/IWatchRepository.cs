using System.Collections.Generic;
using System.Threading.Tasks;
using WatchPortalFunction.Model;

namespace WatchPortalFunction.Repository
{
    public interface IWatchRepository
    {
        Task<Watch> GetWatch(string model);

        Task<List<Watch>> GetAllWatch();
    }
}
