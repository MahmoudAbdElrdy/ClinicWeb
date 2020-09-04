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
    public class ServicesStore : BaseServices,IServicesStore
    {
       
        
        public ServicesStore(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
            : base(unitOfWork,responseDTO,mapper)
        {
           
           
        }
       

        public IResponseDTO GetAll()
        {
        
            try
            {
                var result = _unitOfWork.Store.GetAll();

                var resultList = _mapper.Map<List<StoreViewModel>>(result);
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

        
     public   IResponseDTO Delete(StoreViewModel model)
        {
            try
            {
                var DBmodel = _mapper.Map<Store>(model);
                var entityEntry = _unitOfWork.Store.Remove(DBmodel);

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
                var Dbmodel =await _unitOfWork.Store.GetByIdAsync(id);

                var StoreViewModel = _mapper.Map<StoreViewModel>(Dbmodel);
                _response.Data = StoreViewModel;
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

      public async Task<IResponseDTO> InsertAsync(StoreViewModel entity)
        {
            try
            {
                var ViewModel = _mapper.Map<Store>(entity);
                var Dbmodel =await _unitOfWork.Store.InsertAsync(ViewModel);
               
                int save = _unitOfWork.Commit();

                if (save == 200)
                {
                    var StoreViewModel = _mapper.Map<StoreViewModel>(Dbmodel);
                    _response.Data = StoreViewModel;
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

      public  IResponseDTO Update(StoreViewModel entity)
        {
            try
            {
                var DbStore= _mapper.Map<Store>(entity);
                 _unitOfWork.Store.Update(DbStore);
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
