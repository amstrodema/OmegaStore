using App.Services;
using Microsoft.AspNetCore.Http;
using Store.Data.Interface;
using Store.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Business
{
    public class SlideBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GenericBusiness _genericBusiness;
        public SlideBusiness(IUnitOfWork unitOfWork, GenericBusiness genericBusiness)
        {
            _unitOfWork = unitOfWork;
            _genericBusiness = genericBusiness;
        }
        public async Task<IEnumerable<Slide>> Get()
        {
            return from slide in await _unitOfWork.Slides.GetAll()
                     where slide.StoreID == _genericBusiness.StoreID
                     select new Slide()
                     {
                         ID = slide.ID,
                         Caption = slide.Caption,
                         StoreID = slide.StoreID,
                         Action = slide.Action,
                         Desc = slide.Desc,
                         Image = ImageService.GetSmallImagePath(slide.Image, "Slide"),
                         Link = slide.Link
                     };
        }
        public async Task<ResponseMessage<string>> Create(Slide slide, IFormFile image, Guid userID)
        {
            ResponseMessage<string> responseMessage = new ResponseMessage<string>();
            try
            {
                var slides = await _unitOfWork.Slides.GetByStoreID(_genericBusiness.StoreID);
                if (slides.Count() > 2)
                {
                    responseMessage.StatusCode = 201;
                    responseMessage.Message = "Maximum Slides Reached";
                    return responseMessage;
                }
                slide.ID = Guid.NewGuid();
                slide.Image = await ImageService.SaveImageInFolder(image, slide.ID.ToString(), "Slide");
                slide.StoreID = _genericBusiness.StoreID;
                slide.CreatedBy = userID;
                slide.DateCreated = DateTime.UtcNow.AddHours(1);
                await _unitOfWork.Slides.Create(slide);
                slide.Caption = string.IsNullOrEmpty(slide.Caption) ? string.Empty : slide.Caption;
                slide.Link = string.IsNullOrEmpty(slide.Link) ? string.Empty : slide.Link;
                slide.Action = string.IsNullOrEmpty(slide.Action) ? string.Empty : slide.Action;
                slide.Desc = string.IsNullOrEmpty(slide.Desc) ? string.Empty : slide.Desc;
                if (await _unitOfWork.Commit() > 0)
                {
                    responseMessage.StatusCode = 200;
                    responseMessage.Message = "Slide Added";
                }
                else
                {
                    responseMessage.StatusCode = 201;
                    responseMessage.Message = "Slide Not Added";
                }
            }
            catch (Exception)
            {
                responseMessage.StatusCode = 201;
                responseMessage.Message = "Slide Not Added";
            }

            return responseMessage;
        }

        public async Task<ResponseMessage<string>> Delete(Guid slideID)
        {
            ResponseMessage<string> responseMessage = new ResponseMessage<string>();
            try
            {
                var slide = await _unitOfWork.Slides.Find(slideID);
                _unitOfWork.Slides.Delete(slide);

                if (await _unitOfWork.Commit() > 0)
                {
                    responseMessage.StatusCode = 200;
                    responseMessage.Message = "Slide Deleted";
                }
                else
                {
                    responseMessage.StatusCode = 201;
                    responseMessage.Message = "Slide Not Deleted";
                }
            }
            catch (Exception)
            {
                responseMessage.StatusCode = 201;
                responseMessage.Message = "Slide Not Deleted";
            }
            return responseMessage;
        }
    }
}
