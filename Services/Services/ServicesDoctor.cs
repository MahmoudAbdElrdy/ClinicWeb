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
    public class ServicesDoctor : BaseServices,IServicesDoctor
    {
       
        
        public ServicesDoctor(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
            : base(unitOfWork,responseDTO,mapper)
        {
           
           
        }
       

        public IResponseDTO GetAll()
        {
        
            try
            {
                var result = _unitOfWork.Doctor.GetAll();

                var resultList = _mapper.Map<List<DoctorViewModel>>(result);
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

        
     public   IResponseDTO Delete(DoctorViewModel model)
        {
            try
            {
                var DBmodel = _mapper.Map<Doctor>(model);
                var entityEntry = _unitOfWork.Doctor.Remove(DBmodel);

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
                var Dbmodel =await _unitOfWork.Doctor.GetByIdAsync(id);

                var DoctorViewModel = _mapper.Map<DoctorViewModel>(Dbmodel);
                _response.Data = DoctorViewModel;
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

      public async Task<IResponseDTO> InsertAsync(DoctorViewModel entity)
        {
            try
            {
                var ViewModel = _mapper.Map<Doctor>(entity);
                var Dbmodel =await _unitOfWork.Doctor.InsertAsync(ViewModel);
               
                int save = _unitOfWork.Commit();

                if (save == 200)
                {
                    var DoctorViewModel = _mapper.Map<DoctorViewModel>(Dbmodel);
                    _response.Data = DoctorViewModel;
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

      public  IResponseDTO Update(DoctorViewModel entity)
        {
            try
            {
                var DbDoctor= _mapper.Map<Doctor>(entity);
                 _unitOfWork.Doctor.Update(DbDoctor);
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
