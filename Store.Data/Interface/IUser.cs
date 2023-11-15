using Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Interface
{
    public interface IUser : IGeneric<User>
    {
        Task<User> GetUserByUserNameOrEmail(string usernameOrEmail, Guid storeID);
        Task<User> GetActiveUserByUserName(string username, Guid storeID);
        Task<User> GetUserByUserNameOrEmail(string username, string email, string tel, Guid storeID);
        Task<User> GetActiveUserByUserID(Guid userID, Guid storeID);
        Task<IEnumerable<User>> GetReferrals(string username, Guid storeID);
    }
}
