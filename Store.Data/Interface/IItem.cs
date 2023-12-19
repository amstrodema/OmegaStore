using Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Interface
{
    public interface IItem : IGeneric<Item>
    {
        Task<IEnumerable<Item>> GetByCategID(Guid catID, Guid storeID);
        Task<IEnumerable<Item>> GetByStoreID(Guid storeID);
        Task<Item> GetByTag(string tag, Guid storeID);
        Task<IEnumerable<Item>> GetFeatured(Guid storeID);
        Task<IEnumerable<Item>> GetLatest(Guid storeID);
    }
}
