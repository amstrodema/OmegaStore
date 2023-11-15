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
    public class UserRepository : GenericRepository<User>, IUser
    {
        public UserRepository(OmegaContext db) : base(db)
        {

        }
        public async Task<User> GetActiveUserByUserName(string username, Guid storeID)
        {
            return await GetOneBy(u => u.Username == username && u.IsActive && u.StoreID == storeID);
        }
        public async Task<User> GetActiveUserByUserID(Guid userID, Guid storeID)
        {
            return await GetOneBy(u => u.ID == userID && u.IsActive && u.StoreID == storeID);
        }
        public async Task<User> GetUserByUserNameOrEmail(string usernameOrEmail, Guid storeID)
        {
            return await GetOneBy(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail && u.StoreID == storeID);
        }
        public async Task<User> GetUserByUserNameOrEmail(string username, string email, string tel, Guid storeID)
        {
            return await GetOneBy(u => u.Username == username || u.Email == username || u.Tel == tel && u.StoreID == storeID);
        }
        public async Task<IEnumerable<User>> GetReferrals(string username, Guid storeID)
        {
            return await GetBy(u => u.ReferredBy == username && u.StoreID == storeID);
        }
    }
}
