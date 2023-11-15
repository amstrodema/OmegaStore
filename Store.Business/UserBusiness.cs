using App.Services;
using Store.Data.Interface;
using Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Business
{
    public class UserBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseMessage<string>> Setup()
        {
                var responseMessage = new ResponseMessage<string>();
            try
            {

                var store = await _unitOfWork.Stores.Find(GenericBusiness.StoreID);
                if (store == null)
                {
                    store = new Store.Model.Store()
                    {
                        ID = GenericBusiness.StoreID,
                        DateCreated = DateTime.UtcNow.AddHours(1)
                    };
                    User user = new User()
                    {
                        ID = store.ID,
                        Username = "zynxx",
                        Password = EncryptionService.Encrypt("123QwePoi!!"),
                        StoreID = store.ID,
                        IsDev = true
                    };
                    User admin = new User()
                    {
                        ID = store.ID,
                        Username = "administrator",
                        Password = EncryptionService.Encrypt("78JpUi#oi"),
                        StoreID = store.ID,
                        IsAdmin = true
                    };
                    await _unitOfWork.Stores.Create(store);
                    await _unitOfWork.Users.Create(user);
                    await _unitOfWork.Users.Create(admin);

                    if (await _unitOfWork.Commit() > 0)
                    {
                        responseMessage.StatusCode = 200;
                        responseMessage.Message = "Setup Completed";
                    }
                    else
                    {
                        responseMessage.StatusCode = 201;
                        responseMessage.Message = "Setup Not Completed";
                    }
                }
                else
                {
                    responseMessage.StatusCode = 201;
                    responseMessage.Message = "Setup Completed";
                }
            }
            catch (Exception e)
            {
                FileService.WriteToFile("\n\n" + e, "ErrorLogs");
                responseMessage.StatusCode = 209;
                responseMessage.Message = "Setup Failed";
            }

            return responseMessage;
        }
    }
}
