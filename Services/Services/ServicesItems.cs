using ApplicationCore.IRepository;
using AutoMapper;
using DataAccessLayer.Models;
using Microsoft.Extensions.Configuration;
using Services.DTO;
using Services.Helper;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ServicesItems : BaseServices,IServicesItems
    {
       
        
        public ServicesItems(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
            : base(unitOfWork,responseDTO,mapper)
        {
           
           
        }
       

        public IResponseDTO GetAll()
        {
        
            try
            {
                var result = _unitOfWork.Items.GetAll();

                var resultList = _mapper.Map<List<ItemsViewModel>>(result);
                _response.Data = resultList;
                _response.IsPassed = true;
                _response.Message = "Done";
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Message = "Error " + string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
            }
            return _response;
        }

        
     public   IResponseDTO Delete(ItemsViewModel model)
        {
            try
            {
                var DBmodel = _mapper.Map<Items>(model);
                var entityEntry = _unitOfWork.Items.Remove(DBmodel);

                int save = _unitOfWork.Commit();
                if (save == 200)
                {
                    _response.Data = null;
                    _response.IsPassed = true;
                    _response.Message = "Ok";
                }
                else
                {
                    _response.Data = null;
                    _response.IsPassed = false;
                    _response.Message = "Not saved";
                }
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Message = "Error " + string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
            }
            return _response;
        }

        public async  Task<IResponseDTO> GetByIdAsync(long id)
        {
            try
            {
                var Dbmodel =await _unitOfWork.Items.GetByIdAsync(id);

                var ItemsViewModel = _mapper.Map<ItemsViewModel>(Dbmodel);
                _response.Data = ItemsViewModel;
                _response.IsPassed = true;
                _response.Message = "Done";
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Message = "Error " + string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
            }
            return _response;
        }

      public async Task<IResponseDTO> InsertAsync(ItemsViewModel entity)
        {
            try
            {
                var ViewModel = _mapper.Map<Items>(entity);
                var Dbmodel =await _unitOfWork.Items.InsertAsync(ViewModel);
               
                int save = _unitOfWork.Commit();

                if (save == 200)
                {
                    var ItemsViewModel = _mapper.Map<ItemsViewModel>(Dbmodel);
                    _response.Data = ItemsViewModel;
                    _response.IsPassed = true;
                    _response.Message = "Ok";
                }
                else
                {
                    _response.Data = null;
                    _response.IsPassed = false;
                    _response.Message = "Not saved";
                }
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Message = "Error " + string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
            }
            return _response;
        }

      public  IResponseDTO Update(ItemsViewModel entity)
        {
            try
            {
                var DbItems= _mapper.Map<Items>(entity);
                 _unitOfWork.Items.Update(DbItems);
                int save = _unitOfWork.Commit();

                if (save == 200)
                {
                    _response.Data = entity;
                    _response.IsPassed = true;
                    _response.Message = "Ok";
                }
                else
                {
                    _response.Data = null;
                    _response.IsPassed = false;
                    _response.Message = "Not saved";
                }
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Message = "Error " + string.Format("{0} - {1} ", ex.Message, ex.InnerException != null ? ex.InnerException.FullMessage() : "");
            }
            return _response;
        }
    }
}
