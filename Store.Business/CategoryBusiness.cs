using App.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Store.Data.Interface;
using Store.Model;
using Store.Model.Hybrid;
using Store.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Business
{
    public class CategoryBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GenericBusiness _genericBusiness;
        public CategoryBusiness(IUnitOfWork unitOfWork, GenericBusiness genericBusiness)
        {
            _unitOfWork = unitOfWork;
            _genericBusiness = genericBusiness;
        }
        public async Task<MainVM> GetVM(string t)
        {
            Category categ = new Category();
            try
            {
                categ = await _unitOfWork.Categories.Find(Guid.Parse(t));
            }
            catch (Exception)
            {
               categ = await _unitOfWork.Categories.GetByCategoryTag(t, _genericBusiness.StoreID);
            }
            MainVM mainVM = new MainVM();
            categ.Image = categ.Image != "" ? ImageService.GetSmallImagePath(categ.Image, "CategoryIcons") : "";
            mainVM.Category = categ;
            mainVM.Groups = await _unitOfWork.Groups.GetByStoreID(_genericBusiness.StoreID);
            return mainVM;
        }

        public async Task<IEnumerable<Category>> Get()
        {
            return from cat in await _unitOfWork.Categories.GetByStoreID(_genericBusiness.StoreID)
                   select new Category()
                   {
                       ID = cat.ID,
                       Name = cat.Name,
                       Image = cat.Image != "" ? ImageService.GetSmallImagePath(cat.Image, "CategoryIcons") : "",
                       Tag = cat.Tag
                   };
        }

        public async Task<IEnumerable<CategoryHybrid>> GetHybrids()
        {
            return from cat in await _unitOfWork.Categories.GetByStoreID(_genericBusiness.StoreID)
                   join item in await _unitOfWork.Items.GetAll() on cat.ID equals item.CatID into cats
                   select new CategoryHybrid()
                   {
                       ID = cat.ID,
                       Name = cat.Name,
                       Image = cat.Image != "" ? ImageService.GetSmallImagePath(cat.Image, "CategoryIcons") : "",
                       Tag = cat.Tag,
                       ItemsNo = cats.Count()
                   };
        }
        public async Task<MainVM> GetVM()
        {
            MainVM mainVM = new MainVM();
            mainVM.Groups = await _unitOfWork.Groups.GetByStoreID(_genericBusiness.StoreID);
            mainVM.Categories = from cat in await _unitOfWork.Categories.GetByStoreID(_genericBusiness.StoreID)
                                join grp in mainVM.Groups on cat.GroupID equals grp.ID
                                select new Category()
                                {
                                    ID = cat.ID,
                                    Name = cat.Name,
                                    Group = grp.Name,
                                    GroupID = grp.ID,
                                    Image = cat.Image != "" ? ImageService.GetSmallImagePath(cat.Image, "CategoryIcons") :"",
                                    Tag = cat.Tag,
                                    DateCreated = cat.DateModified == default ? cat.DateCreated : cat.DateModified
                                };
           
            mainVM.Categories = mainVM.Categories.OrderBy(p => p.Name);
            mainVM.Groups = mainVM.Groups.OrderBy(p => p.Name);
            return mainVM;
        }
        public async Task<ResponseMessage<string>> Create(MainVM mainVM, User user, IFormFile image)
        {
            ResponseMessage<string> responseMessage = new ResponseMessage<string>();
            try
            {
                Category category = mainVM.Category;
                category.Tag = GenericService.GetTag(category.Name);
                var thisCat = await _unitOfWork.Categories.GetByCategoryTag(category.Tag.Trim(), _genericBusiness.StoreID);

                if (thisCat != null)
                {
                    responseMessage.StatusCode = 201;
                    responseMessage.Message = "Category exists";
                    return responseMessage;
                }

                if (image == null)
                {
                    responseMessage.StatusCode = 201;
                    responseMessage.Message = "Not Saved! Add a category image.";
                    return responseMessage;
                }

                if (string.IsNullOrEmpty(mainVM.Category.Name))
                {
                    responseMessage.StatusCode = 201;
                    responseMessage.Message = "Not Saved! Add a category name.";
                    return responseMessage;
                }

                if (category.GroupID == default)
                {
                    if (string.IsNullOrEmpty(mainVM.Group.Name))
                    {
                        responseMessage.StatusCode = 201;
                        responseMessage.Message = "Not Saved! add a group name.";
                        return responseMessage;
                    }
                    mainVM.Group.Tag = GenericService.GetTag(mainVM.Group.Name);
                    var thisGroup = await _unitOfWork.Groups.GetByGroupTag(mainVM.Group.Tag.Trim(), _genericBusiness.StoreID);

                    if (thisGroup != null)
                    {
                        category.GroupID = thisGroup.ID;
                    }
                    else
                    {
                        mainVM.Group.ID = Guid.NewGuid();
                        category.GroupID = mainVM.Group.ID;
                        mainVM.Group.DateCreated = DateTime.UtcNow.AddHours(1);
                        mainVM.Group.CreatedBy = user.ID;

                        mainVM.Group.IsActive = true;
                        mainVM.Group.StoreID = _genericBusiness.StoreID;

                        await _unitOfWork.Groups.Create(mainVM.Group);
                    }                    
                }

                category.ID = Guid.NewGuid();
                category.Image = await ImageService.SaveImageInFolder(image, category.ID.ToString(), "CategoryIcons");
                category.CreatedBy = user.ID;
                category.DateCreated = DateTime.UtcNow.AddHours(1);
                category.StoreID = _genericBusiness.StoreID;
                category.IsActive = true;
                await _unitOfWork.Categories.Create(category);

                if (await _unitOfWork.Commit() > 0)
                {
                    responseMessage.StatusCode = 200;
                    responseMessage.Message = "Category Saved!";
                }
                else
                {
                    responseMessage.StatusCode = 201;
                    responseMessage.Message = "Category Not Saved!";
                }
            }
            catch (Exception)
            {
                responseMessage.StatusCode = 201;
                responseMessage.Message = "Category Not Saved!";
            }
            return responseMessage;
        }
        public async Task<ResponseMessage<string>> Modify(MainVM mainVM, User user, IFormFile image)
        {
            ResponseMessage<string> responseMessage = new ResponseMessage<string>();
            try
            {
                var updatedCat = mainVM.Category;
                Category category = await _unitOfWork.Categories.GetByCategoryTag(mainVM.Category.Tag, _genericBusiness.StoreID);
                //= await _unitOfWork.Categories.Find(thisCat.ID);
                if(!category.IsDefault)
                category.Name = updatedCat.Name;
                category.Group = updatedCat.Group;
                if (!category.IsDefault)
                    category.GroupID = updatedCat.GroupID;

                if (category == null)
                {
                    responseMessage.StatusCode = 201;
                    responseMessage.Message = "Category does not exist";
                    return responseMessage;
                }

                if (image == null && string.IsNullOrEmpty(category.Image))
                {
                    responseMessage.StatusCode = 201;
                    responseMessage.Message = "Not Saved! Add a category image.";
                    return responseMessage;
                }

                if (string.IsNullOrEmpty(category.Name))
                {
                    responseMessage.StatusCode = 201;
                    responseMessage.Message = "Not Saved! Add a category name.";
                    return responseMessage;
                }

                if (category.GroupID == default)
                {
                    if (string.IsNullOrEmpty(mainVM.Group.Name))
                    {
                        responseMessage.StatusCode = 201;
                        responseMessage.Message = "Not Saved! add a group name.";
                        return responseMessage;
                    }
                    mainVM.Group.Tag = GenericService.GetTag(mainVM.Group.Name);
                    var thisGroup = await _unitOfWork.Groups.GetByGroupTag(mainVM.Group.Tag.Trim(), _genericBusiness.StoreID);

                    if (thisGroup != null)
                    {
                        category.GroupID = thisGroup.ID;
                    }
                    else
                    {
                        mainVM.Group.ID = Guid.NewGuid();
                        category.GroupID = mainVM.Group.ID;
                        mainVM.Group.DateCreated = DateTime.UtcNow.AddHours(1);
                        mainVM.Group.CreatedBy = user.ID;

                        mainVM.Group.IsActive = true;
                        mainVM.Group.StoreID = _genericBusiness.StoreID;

                        await _unitOfWork.Groups.Create(mainVM.Group);
                    }                    
                }

                category.DateModified = DateTime.UtcNow.AddHours(1);
                category.ModifiedBy = user.ID;

                if (category.Tag != GenericService.GetTag(category.Name))
                {
                    category.Tag = GenericService.GetTag(category.Name);
                   var thisCat = await _unitOfWork.Categories.GetByCategoryTag(category.Tag, _genericBusiness.StoreID);
                    if (thisCat != null)
                    {
                        responseMessage.StatusCode = 201;
                        responseMessage.Message = "Category exists!";
                        return responseMessage;
                    }
                }

                _unitOfWork.Categories.Update(category);

                if (await _unitOfWork.Commit() > 0)
                {
                    responseMessage.StatusCode = 200;
                    responseMessage.Message = "Category Modified!";
                }
                else
                {
                    responseMessage.StatusCode = 201;
                    responseMessage.Message = "Category Not Modified!";
                }
            }
            catch (Exception e)
            {
                responseMessage.StatusCode = 201;
                responseMessage.Message = "Category Not Modified!";
                FileService.WriteToFile("\n\n" + e, "ErrorLogs");
            }
            return responseMessage;
        }
        //public async Task<ResponseMessage<string>> Delete(string t)
        //{
        //    var responseMessage = new ResponseMessage<string>();
        //    var cat = new Category();
        //    try
        //    {
        //        cat = await _unitOfWork.Categories.Find(Guid.Parse(t));
        //    }
        //    catch (Exception)
        //    {
        //        cat = await _unitOfWork.Categories.GetByCategoryTag(t, _genericBusiness.StoreID);
        //    }

        //    var items = await _unitOfWork.Items.GetByCategID(cat.ID);
        //}
    }
}
