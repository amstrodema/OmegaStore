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
        public async Task<MainVM> GetFromCategory(string t)
        {
            MainVM mainVM = new MainVM();
            try
            {
                var cat = await _unitOfWork.Categories.GetByCategoryTag(t, _genericBusiness.StoreID);
                mainVM.Category = cat;
                mainVM.Stocks = from item in await _unitOfWork.Items.GetByStoreID(_genericBusiness.StoreID)
                                where item.CatID == cat.ID && item.IsActive && item.Currency == GenericBusiness.ShoppingCurrency
                                join review in await _unitOfWork.Reviews.GetAll() on item.ID equals review.ItemID into reviews
                                select new Item()
                                {
                                    CatID = item.CatID,
                                    Tag = item.Tag,
                                    Image = ImageService.GetLargeImagePath(item.Image, "ItemImage"),
                                    Currency = item.Currency,
                                    CurrencySymbol = item.CurrencySymbol,
                                    Title = item.Title,
                                    OldPrice = item.OldPrice,
                                    Price = item.Price,
                                    ID = item.ID,
                                    Rating = reviews.Sum(p => p.Rating) / (reviews.Count() < 10 ? 10 : reviews.Count()),
                                    IsRecent = item.IsRecent,
                                    IsFeatured = item.IsFeatured,
                                    Reviews = reviews.Count()
                                };
                mainVM.CategoryHybrids = await _categoryBusiness.GetHybrids();
            }
            catch (Exception)
            {
                throw;
            }

            return mainVM;
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
            mainVM.Reviews =(from review in await _unitOfWork.Reviews.GetByItemID(_genericBusiness.StoreID, item.ID)
                            join user in await _unitOfWork.Users.GetAll() on review.UserID equals user.ID into users
                            from thisUser in users.DefaultIfEmpty()
                            select new Review()
                            {
                                ID = review.ID,
                                Name = review.UserID != default ? thisUser.Username : review.Name,
                                Message = review.Message,
                                DateCreated = review.DateCreated,
                                Rating = review.Rating
                            }).OrderByDescending(o=> o.DateCreated);
            var traffic = mainVM.Reviews.Count() < 10 ? 10 : mainVM.Reviews.Count();
            mainVM.Ratings = mainVM.Reviews.Sum(p => p.Rating) / traffic;
            return mainVM;
        }
        public async Task<CheckOutVM> GetCart(OrderVM[] ? orderItems)
        {
            CheckOutVM checkOutVM = new CheckOutVM();
            if (orderItems == null)
            {
                return checkOutVM;
            }
           var orders = from orderItem in orderItems
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
            checkOutVM.Orders = orders;
            return checkOutVM;
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

                mainVM.Favourite = (from fave in faves
                                   join item in await _unitOfWork.Items.GetByStoreIDAndCurrency(_genericBusiness.StoreID, GenericBusiness.ShoppingCurrency) on fave equals item.ID
                                   join review in await _unitOfWork.Reviews.GetAll() on item.ID equals review.ItemID into reviews
                                   select new Item()
                                   {
                                       CatID = item.CatID,
                                       Tag = item.Tag,
                                       Image = ImageService.GetLargeImagePath(item.Image, "ItemImage"),
                                       Currency = item.Currency,
                                       CurrencySymbol = item.CurrencySymbol,
                                       Title = item.Title,
                                       OldPrice = item.OldPrice,
                                       Price = item.Price,
                                       ID = item.ID,
                                       Rating = reviews.Sum(p => p.Rating) / (reviews.Count() < 10 ? 10 : reviews.Count()),
                                       IsRecent = item.IsRecent,
                                       IsFeatured = item.IsFeatured,
                                       Reviews = reviews.Count()
                                   }).OrderByDescending(o=> o.DateCreated);

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
            mainVM.Featured = from item in await _unitOfWork.Items.GetFeatured(_genericBusiness.StoreID)
                              where item.Currency == GenericBusiness.ShoppingCurrency
                              join review in await _unitOfWork.Reviews.GetAll() on item.ID equals review.ItemID into reviews
                              select new Item()
                              {
                                  CatID = item.CatID,
                                  Tag = item.Tag,
                                  Image = ImageService.GetLargeImagePath(item.Image, "ItemImage"),
                                  Currency = item.Currency,
                                  CurrencySymbol = item.CurrencySymbol,
                                  Title = item.Title,
                                  OldPrice = item.OldPrice,
                                  Price = item.Price,
                                  ID = item.ID,
                                  Rating = reviews.Sum(p => p.Rating) / (reviews.Count() < 10 ? 10 : reviews.Count()),
                                  IsRecent = item.IsRecent,
                                  IsFeatured = item.IsFeatured,
                                  Reviews = reviews.Count()
                              };

            mainVM.Latest = from item in await _unitOfWork.Items.GetLatest(_genericBusiness.StoreID)
                            where item.Currency == GenericBusiness.ShoppingCurrency
                            join review in await _unitOfWork.Reviews.GetAll() on item.ID equals review.ItemID into reviews
                            select new Item()
                            {
                                CatID = item.CatID,
                                Tag = item.Tag,
                                Image = ImageService.GetLargeImagePath(item.Image, "ItemImage"),
                                Currency = item.Currency,
                                CurrencySymbol = item.CurrencySymbol,
                                Title = item.Title,
                                OldPrice = item.OldPrice,
                                Price = item.Price,
                                ID = item.ID,
                                Rating = reviews.Sum(p => p.Rating) / (reviews.Count() < 10 ? 10 : reviews.Count()),
                                IsRecent = item.IsRecent,
                                IsFeatured = item.IsFeatured,
                                Reviews = reviews.Count()
                            };

            mainVM.Slides = from slide in await _unitOfWork.Slides.GetAll()
                            where slide.StoreID == _genericBusiness.StoreID
                            select new Slide()
                            {
                                ID = slide.ID,
                                Caption = slide.Caption,
                                StoreID = slide.StoreID,
                                Action = slide.Action,
                                Desc = slide.Desc,
                                Image = ImageService.GetLargeImagePath(slide.Image, "Slide"),
                                Link = slide.Link
                            };

            mainVM.Brands = from brand in await _unitOfWork.Brands.GetAll()
                            where brand.StoreID == _genericBusiness.StoreID
                            select new Brand()
                            {
                                ID = brand.ID,
                                Tag = brand.Tag,
                                Logo = ImageService.GetLargeImagePath(brand.Logo,"Brand")
                            };
            var offers = await _unitOfWork.Offers.GetAll();
            mainVM.Offers = (from offer in offers
                             where offer.StoreID == _genericBusiness.StoreID && offer.IsActive && offer.IsHomepage && !offer.IsAdmin
                             select new Offer()
                             {
                                 ID = offer.ID,
                                 Caption = offer.Caption,
                                 DiscountCaption = offer.DiscountCaption,
                                 StoreID = offer.StoreID,
                                 Action = offer.Action,
                                 Description = offer.Description,
                                 Tag = offer.Tag,
                                 Image = ImageService.GetLargeImagePath(offer.Image, "Offer"),
                                 Link = offer.Link,
                                 DateCreated = offer.DateCreated
                             }).Take(3).ToList();

            mainVM.Offer = offers.FirstOrDefault(p => p.IsActive && p.IsAdmin);

            return mainVM;
        }
        public async Task<MainVM> GetVMForShop()
        {
            MainVM mainVM = new MainVM();
            mainVM.CategoryHybrids = await _categoryBusiness.GetHybrids();
            mainVM.Stocks = (from item in await _unitOfWork.Items.GetByStoreID(_genericBusiness.StoreID)
                              where item.Currency == GenericBusiness.ShoppingCurrency
                              join review in await _unitOfWork.Reviews.GetAll() on item.ID equals review.ItemID into reviews
                              select new Item()
                              {
                                  CatID = item.CatID,
                                  Tag = item.Tag,
                                  Image = ImageService.GetLargeImagePath(item.Image, "ItemImage"),
                                  Currency = item.Currency,
                                  CurrencySymbol = item.CurrencySymbol,
                                  Title = item.Title,
                                  OldPrice = item.OldPrice,
                                  Price = item.Price,
                                  ID = item.ID,
                                  Rating = reviews.Sum(p => p.Rating) / (reviews.Count() < 10 ? 10 : reviews.Count()),
                                  IsRecent = item.IsRecent,
                                  IsFeatured = item.IsFeatured,
                                  Reviews = reviews.Count(),
                                  DateCreated = item.DateCreated
                              }).OrderByDescending(o=> o.DateCreated);
            mainVM.Features = await _unitOfWork.Features.GetByStoreID(_genericBusiness.StoreID);

            return mainVM;
        }

        public async Task<MainVM> AddReview(Review review, Guid userID)
        {
            MainVM mainVM = new MainVM();
            try
            {
                review.ID = Guid.NewGuid();
                review.CreatedBy = userID;
                review.UserID = userID;
                review.StoreID = _genericBusiness.StoreID;
                review.DateCreated = DateTime.UtcNow.AddHours(1);
                review.Email = string.IsNullOrEmpty(review.Email) ? string.Empty : review.Email;
                review.Name = string.IsNullOrEmpty(review.Name) ? string.Empty : review.Name;

                var oldReview = await _unitOfWork.Reviews.GetByItemIDAndUserID(review.ItemID, userID);

                if (userID == default)
                {
                    oldReview = await _unitOfWork.Reviews.GetByItemIDAndUserEmail(review.ItemID, review.Email);
                }
                else
                {
                    var user = await _unitOfWork.Users.Find(userID);
                    review.Name = user.Fname;
                }

                if (oldReview != null)
                {
                    _unitOfWork.Reviews.Delete(oldReview);
                }

                await _unitOfWork.Reviews.Create(review);
                await _unitOfWork.Commit();

            }
            catch (Exception)
            {
            }

            mainVM.Reviews = (from thisReview in await _unitOfWork.Reviews.GetByItemID(_genericBusiness.StoreID, review.ItemID)
                             join user in await _unitOfWork.Users.GetAll() on thisReview.UserID equals user.ID into users
                             from thisUser in users.DefaultIfEmpty()
                             select new Review()
                             {
                                 ID = thisReview.ID,
                                 Name = thisReview.UserID != default ? thisUser.Username : thisReview.Name,
                                 Message = thisReview.Message,
                                 DateCreated = thisReview.DateCreated,
                                 Rating = thisReview.Rating
                             }).OrderByDescending(p=> p.DateCreated);
            var traffic = mainVM.Reviews.Count() < 10 ? 10 : mainVM.Reviews.Count();
            mainVM.Ratings = mainVM.Reviews.Sum(p => p.Rating) / traffic;

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

        public async Task<ResponseMessage<string>> CheckOut(Guid itemID, Guid userID)
        {
            ResponseMessage<string> responseMessage = new ResponseMessage<string>();

            return responseMessage;
        }
    }
}
