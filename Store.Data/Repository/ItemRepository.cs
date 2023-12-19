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
    public class ItemRepository : GenericRepository<Item>, IItem
    {
        public ItemRepository(OmegaContext db) : base(db)
        {

        }
        public async Task<IEnumerable<Item>> GetByCategID(Guid catID, Guid storeID)
        {
            return await GetBy(o => o.CatID == catID && o.StoreID == storeID);
        }
        public async Task<Item> GetByTag(string tag, Guid storeID)
        {
            return await GetOneBy(o => o.Tag == tag && o.StoreID == storeID);
        }
        public async Task<IEnumerable<Item>> GetByStoreID(Guid storeID)
        {
            return await GetBy(o =>  o.StoreID == storeID);
        }
        public async Task<IEnumerable<Item>> GetFeatured(Guid storeID)
        {
            return await GetBy(o =>  o.StoreID == storeID && o.IsFeatured);
        }
        public async Task<IEnumerable<Item>> GetLatest(Guid storeID)
        {
            return await GetBy(o =>  o.StoreID == storeID && o.IsRecent);
        }
    }
}
