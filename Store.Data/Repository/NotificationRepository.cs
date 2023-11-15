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
    public class NotificationRepository : GenericRepository<Notification>, INotification
    {
        public NotificationRepository(OmegaContext db) : base(db)
        {

        }
        public async Task<int> CheckForNotification(Guid userID)
        {
            return (await GetBy(p => p.ReceiverID == userID && !p.IsRead)).Count();
        }
    }
}
