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
    public class CategoryRepository : GenericRepository<Category>, ICategory
    {
        public CategoryRepository(OmegaContext db) : base(db)
        {

        }
        public async Task<IEnumerable<Category>> GetByStoreID(Guid storeID)
        {
            return await GetBy(p => p.StoreID == storeID);
        }
        public async Task<Category> GetByCategoryTag(string tag, Guid storeID)
        {
            return await GetOneBy(p => p.Tag == tag && p.StoreID == storeID);
        }
    }
}
