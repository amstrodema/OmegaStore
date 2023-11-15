using Microsoft.EntityFrameworkCore;
using Store.Data.Interface;
using Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Repository
{
    public class LoginMonitorRepository : GenericRepository<LoginMonitor>, ILoginMonitor
    {
        public LoginMonitorRepository(OmegaContext db) : base(db)
        {

        }
        public async Task<LoginMonitor> GetMonitorByUserID(Guid userID, Guid appID, Guid storeID)
        {
            return await GetOneBy(p => p.UserID == userID && p.ClientCode == appID && p.StoreID == storeID);
        }
    }
}
