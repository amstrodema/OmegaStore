using App.Services;
using Microsoft.AspNetCore.Http;
using Store.Data.Interface;
using Store.Model;
using Store.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Business
{
    public class StoreBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GenericBusiness _genericBusiness;
        private readonly GeneralBusiness _generalBusiness;
        private readonly CategoryBusiness _categoryBusiness;
        public StoreBusiness(IUnitOfWork unitOfWork, GenericBusiness genericBusiness, GeneralBusiness generalBusiness, CategoryBusiness categoryBusiness)
        {
            _unitOfWork = unitOfWork;
            _genericBusiness = genericBusiness;
            _generalBusiness = generalBusiness;
            _categoryBusiness = categoryBusiness;   
        }
        public async Task<IEnumerable<Item>> Get() => await _unitOfWork.Items.GetByStoreID(_genericBusiness.StoreID);
        public async Task<MainVM> GetVM()
        {
            MainVM mainVM = new MainVM();
            mainVM.Stocks = await _unitOfWork.Items.GetByStoreID(_genericBusiness.StoreID);
            mainVM.Categories = await _unitOfWork.Categories.GetByStoreID(_genericBusiness.StoreID);
            //mainVM.Categories = await _unitOfWork.Categories.GetByStoreID(_genericBusiness.StoreID);
            return mainVM;
        }
        public async Task<MainVM> GetVM(string t)
        {
            Item item = new Item();
            try
            {
                item = await _unitOfWork.Items.Find(Guid.Parse(t));
            }
            catch (Exception)
            {
                item = await _unitOfWork.Items.GetByTag(t, _genericBusiness.StoreID);
            }
            MainVM mainVM = new MainVM();
            mainVM.Stock = _generalBusiness.AttachImage(item);
            mainVM.Categories = await _unitOfWork.Categories.GetByStoreID(_genericBusiness.StoreID);
            return mainVM;
        }

        public async Task<ResponseMessage<string>> Create(ItemVM itemVM, User user, IFormFile img, IFormFile img1, IFormFile img2, IFormFile img3, IFormFile img4)
        {
            ResponseMessage<string> responseMessage = new ResponseMessage<string>();
            try
            {
                var cat = await _unitOfWork.Categories.Find(itemVM.Category);

                if (cat == null) { responseMessage.StatusCode = 201; return responseMessage; }
                Guid thisID;

                Item item = new Item()
                {
                    Brief = itemVM.Brief,
                    Currency = itemVM.Currency,
                    Description = itemVM.Desc,
                    OldPrice = itemVM.OldPrice,
                    Price = itemVM.Price,
                    Title = itemVM.Title,
                    Tag = GenericService.GetTag(itemVM.Title),
                    StoreID = _genericBusiness.StoreID,
                    ID = thisID = Guid.NewGuid(),
                    CatID = itemVM.Category,
                    CreatedBy = user.ID,
                    CurrencySymbol = itemVM.Currency == "NGN" ? "₦" : "$",
                    IsActive = true,
                    GroupID = cat.GroupID,
                    DateCreated = DateTime.UtcNow.AddHours(1),
                    IsApproved = true,
                    Image = img != null ? await ImageService.SaveImageInFolder(img, thisID.ToString(), "ItemImage") : "",
                    Image1 = img1 != null ? await ImageService.SaveImageInFolder(img1, thisID.ToString() + "1", "ItemImage") : "",
                    Image2 = img2 != null ? await ImageService.SaveImageInFolder(img2, thisID.ToString() + "2", "ItemImage") : "",
                    Image3 = img3 != null ? await ImageService.SaveImageInFolder(img3, thisID.ToString() + "3", "ItemImage") : "",
                    Image4 = img4 != null ? await ImageService.SaveImageInFolder(img4, thisID.ToString() + "4", "ItemImage") : "",
                    IsRecent = true
                };
                var thisItem = await _unitOfWork.Items.GetByTag(item.Tag, _genericBusiness.StoreID);
                if (thisItem != null)
                {
                    responseMessage.StatusCode = 201;
                    responseMessage.Message = "Item Exists already!";
                    return responseMessage;
                }
                await _unitOfWork.Items.Create(item);

                if (itemVM.Features != null)
                    foreach (var featurePicker in itemVM.Features)
                    {
                        await _unitOfWork.Features.Create(new Feature()
                        {
                            Name = featurePicker.Name,
                            StoreID = _genericBusiness.StoreID,
                            ItemID = item.ID,
                            ID = Guid.NewGuid(),
                            CreatedBy = user.ID,
                            DateCreated = DateTime.UtcNow.AddHours(1),
                            IsActive = true,
                            IsApproved = true,
                            Options = featurePicker.Option
                        }
                            );

                    }

                if (await _unitOfWork.Commit() > 0)
                {
                    responseMessage.StatusCode = 200;
                    responseMessage.Message = "Item Saved!";
                }
                else
                {
                    responseMessage.StatusCode = 201;
                    responseMessage.Message = "Item Not Saved!";
                }

            }
            catch (Exception e)
            {
                responseMessage.StatusCode = 201;
                responseMessage.Message = "Item Not Saved!";
                FileService.WriteToFile("\n\n" + e, "ErrorLogs");
            }
            return responseMessage;
        }
        public async Task<ResponseMessage<string>> Modify(ItemVM itemVM, User user, IFormFile img, IFormFile img1, IFormFile img2, IFormFile img3, IFormFile img4)
        {
            ResponseMessage<string> responseMessage = new ResponseMessage<string>();
            try
            {
                var cat = await _unitOfWork.Categories.Find(itemVM.Category);
                var item = await _unitOfWork.Items.Find(itemVM.ID);

                if (cat == null) { responseMessage.StatusCode = 201; return responseMessage; }
                if (item == null) { responseMessage.StatusCode = 201; return responseMessage; }

                itemVM.Img2 = string.IsNullOrEmpty(itemVM.Img2) ? "" : item.Image1;
                itemVM.Img3 = string.IsNullOrEmpty(itemVM.Img2) ? "" : item.Image2;
                itemVM.Img4 = string.IsNullOrEmpty(itemVM.Img2) ? "" : item.Image3;
                itemVM.Img5 = string.IsNullOrEmpty(itemVM.Img2) ? "" : item.Image4;

                item.Brief = itemVM.Brief;
                item.Currency = itemVM.Currency;
                item.Description = itemVM.Desc;
                item.OldPrice = itemVM.OldPrice;
                item.Price = itemVM.Price;
                item.Title = itemVM.Title;
                item.StoreID = _genericBusiness.StoreID;
                item.CatID = itemVM.Category;
                item.ModifiedBy = user.ID;
                item.CurrencySymbol = itemVM.Currency == "NGN" ? "₦" : "$";
                item.GroupID = cat.GroupID;
                item.DateModified = DateTime.UtcNow.AddHours(1);
                item.Image = img != null ? await ImageService.SaveImageInFolder(img, item.ID.ToString(), "ItemImage") : item.Image;
                item.Image1 = img1 != null ? await ImageService.SaveImageInFolder(img1, item.ID.ToString() + "1", "ItemImage") : itemVM.Img2;
                item.Image2 = img2 != null ? await ImageService.SaveImageInFolder(img2, item.ID.ToString() + "2", "ItemImage") : itemVM.Img3;
                item.Image3 = img3 != null ? await ImageService.SaveImageInFolder(img3, item.ID.ToString() + "3", "ItemImage") : itemVM.Img4;
                item.Image4 = img4 != null ? await ImageService.SaveImageInFolder(img4, item.ID.ToString() + "4", "ItemImage") : itemVM.Img5;

                if (GenericService.GetTag(itemVM.Title) != item.Tag)
                {
                    item.Tag = GenericService.GetTag(itemVM.Title);

                    var thisItem = await _unitOfWork.Items.GetByTag(item.Tag, _genericBusiness.StoreID);
                    if (thisItem != null)
                    {
                        responseMessage.StatusCode = 201;
                        responseMessage.Message = "Item Exists already!";
                        return responseMessage;
                    }
                }


                _unitOfWork.Items.Update(item);

                var features = await _unitOfWork.Features.GetByItemID(item.ID);
                foreach (var feature in features)
                {
                    _unitOfWork.Features.Delete(feature);
                }

                if (itemVM.Features != null)
                    foreach (var featurePicker in itemVM.Features)
                    {
                        await _unitOfWork.Features.Create(new Feature()
                        {
                            Name = featurePicker.Name,
                            StoreID = _genericBusiness.StoreID,
                            ItemID = item.ID,
                            ID = Guid.NewGuid(),
                            CreatedBy = user.ID,
                            DateCreated = DateTime.UtcNow.AddHours(1),
                            IsActive = true,
                            IsApproved = true,
                            Options = featurePicker.Option
                        }
                            );

                    }

                if (await _unitOfWork.Commit() > 0)
                {
                    responseMessage.StatusCode = 200;
                    responseMessage.Message = "Item Modifed!";
                }
                else
                {
                    responseMessage.StatusCode = 201;
                    responseMessage.Message = "Item Not Modified!";
                }

            }
            catch (Exception e)
            {
                responseMessage.StatusCode = 201;
                responseMessage.Message = "Item Not Modified!";
                FileService.WriteToFile("\n\n" + e, "ErrorLogs");
            }
            return responseMessage;
        }

        public async Task<MainVM> GetItem(string t)
        {
            var item = await _unitOfWork.Items.GetByTag(t, _genericBusiness.StoreID);
            _generalBusiness.AttachImage(item);
            MainVM mainVM = new MainVM();
            mainVM.Stock = item;
            mainVM.Features = await _unitOfWork.Features.GetByItemID(item.ID);
            return mainVM;
        }
        public async Task<IEnumerable<OrderVM>> GetCart(OrderVM[] ? orderItems)
        {
            if (orderItems == null)
            {
                return Enumerable.Empty<OrderVM>();
            }
           return from orderItem in orderItems
                      join item in await _unitOfWork.Items.GetByStoreID(_genericBusiness.StoreID) on orderItem.ID equals item.ID
                      select new OrderVM()
                      {
                          ID = orderItem.ID,
                          Image = ImageService.GetSmallImagePath(item.Image, "ItemImage"),
                          ItemName = item.Title,
                          Qty = orderItem.Qty,
                          Features = orderItem.Features,
                          Price = item.Price,
                          CurrenySymbol = item.CurrencySymbol,
                          Tag = item.Tag
                      };
        }
        public async Task<MainVM> GetVMForFave(Guid[] ? faves)
        {
            MainVM mainVM = new MainVM();

            try
            {
                if (faves == null)
                {
                    throw new Exception();
                }

                mainVM.Favourite = from fave in faves
                                   join item in await _unitOfWork.Items.GetAll() on fave equals item.ID
                                   select item;

                mainVM.Favourite = _generalBusiness.AttachImage(mainVM.Favourite);
            }
            catch (Exception)
            {
                throw;
            }

            return mainVM;
        }
        public async Task<MainVM> GetVMForHome()
        {
            MainVM mainVM = new MainVM();
            mainVM.CategoryHybrids = await _categoryBusiness.GetHybrids();
            mainVM.Featured = _generalBusiness.AttachImage(await _unitOfWork.Items.GetFeatured(_genericBusiness.StoreID)).Where(c => c.Currency == GenericBusiness.ShoppingCurrency);
            mainVM.Latest = _generalBusiness.AttachImage(await _unitOfWork.Items.GetLatest(_genericBusiness.StoreID)).Where(c=> c.Currency == GenericBusiness.ShoppingCurrency);

            return mainVM;
        }
        public async Task<MainVM> GetVMForShop()
        {
            MainVM mainVM = new MainVM();
            mainVM.CategoryHybrids = await _categoryBusiness.GetHybrids();
            mainVM.Stocks = _generalBusiness.AttachImage(await _unitOfWork.Items.GetByStoreID(_genericBusiness.StoreID)).Where(c => c.Currency == GenericBusiness.ShoppingCurrency);
            mainVM.Features = await _unitOfWork.Features.GetByStoreID(_genericBusiness.StoreID);

            return mainVM;
        }

        public async Task<int> MarkAsFave(Guid itemID, Guid userID)
        {
            try
            {
                var fave = await _unitOfWork.Favourites.GetByUserAndItemID(itemID, userID);
                if (fave != null)
                    _unitOfWork.Favourites.Delete(fave);
                else
                    await _unitOfWork.Favourites.Create(new Favourite()
                    {
                        ID = Guid.NewGuid(),
                        DateCreated = DateTime.UtcNow.AddHours(1),
                        StoreID = _genericBusiness.StoreID,
                        ItemID = itemID,
                        CreatedBy = userID,
                        UserID = userID
                    });

                if (await _unitOfWork.Commit() > 0)
                {
                    return 1;
                }
            }
            catch (Exception)
            {
                return 0;
            }
            return 0;
        }
    }
}
