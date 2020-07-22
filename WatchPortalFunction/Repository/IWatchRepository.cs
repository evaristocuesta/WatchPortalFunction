using WatchPortalFunction.Model;

namespace WatchPortalFunction.Repository
{
    public interface IWatchRepository
    {
        Watch GetWatch(string model);
    }
}
