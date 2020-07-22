using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchPortalFunction.Model;

namespace WatchPortalFunction.Repository
{
    public class WatchRepository : IWatchRepository
    {
        private Dictionary<string, Watch> _watchs;

        public WatchRepository()
        {
            _watchs = new Dictionary<string, Watch>(){
                {
                    "aaa",
                    new Watch()
                    {
                        Manufacturer = "Seiko",
                        Model = "Senior A320",
                        CaseType = "Solid",
                        Bezel = "Titanium",
                        Dial = "Roman",
                        CaseFinish = "Silver",
                        Jewels = 15
                    }
                },
                {
                    "bbb",
                    new Watch()
                    {
                        Manufacturer = "Seiko",
                        Model = "Junior A320",
                        CaseType = "Solid",
                        Bezel = "Titanium",
                        Dial = "Roman",
                        CaseFinish = "Silver",
                        Jewels = 5
                    }
                },
                {
                    "ccc",
                    new Watch()
                    {
                        Manufacturer = "Rolex",
                        Model = "Goldfish",
                        CaseType = "Solid",
                        Bezel = "Gold",
                        Dial = "Roman",
                        CaseFinish = "Gold",
                        Jewels = 20
                    }
                }

            };
        }

        public async Task<List<Watch>> GetAllWatch()
        {
            var t = Task.Run(() => { 
                return _watchs.Values.ToList(); 
            });
            return await t;
        }

        public async Task<Watch> GetWatch(string model)
        {
            var t = Task.Run(() =>
            {
                Watch watch;
                _watchs.TryGetValue(model, out watch);
                return watch;
            });
            return await t;
        }
    }
}
