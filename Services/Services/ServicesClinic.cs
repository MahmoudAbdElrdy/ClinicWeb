using ApplicationCore;
using ApplicationCore.IRepository;
using ApplicationCore.Repository;
using AutoMapper;

using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.Extensions.Configuration;
using Services.DTO;
using Services.Helper;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ServicesClinic : BaseServices,IServicesClinic
    {
       
        
        public ServicesClinic(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
            : base(unitOfWork,responseDTO,mapper)
        {
           
           
        }
       

        public IResponseDTO GetAll()
        {
        
            try
            {
                var result = _unitOfWork.Clinic.GetAll();

                var resultList = _mapper.Map<List<ClinicViewModel>>(result);
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

        
     public   IResponseDTO Delete(ClinicViewModel model)
        {
            try
            {
                var DBmodel = _mapper.Map<Clinic>(model);
                var entityEntry = _unitOfWork.Clinic.Remove(DBmodel);

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
                var Dbmodel =await _unitOfWork.Clinic.GetByIdAsync(id);

                var ClinicViewModel = _mapper.Map<ClinicViewModel>(Dbmodel);
                _response.Data = ClinicViewModel;
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

      public async Task<IResponseDTO> InsertAsync(ClinicViewModel entity)
        {
            try
            {
                var ViewModel = _mapper.Map<Clinic>(entity);
                var Dbmodel =await _unitOfWork.Clinic.InsertAsync(ViewModel);
               
                int save = _unitOfWork.Commit();

                if (save == 200)
                {
                    var ClinicViewModel = _mapper.Map<ClinicViewModel>(Dbmodel);
                    _response.Data = ClinicViewModel;
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

      public  IResponseDTO Update(ClinicViewModel entity)
        {
            try
            {
                var DbClinic= _mapper.Map<Clinic>(entity);
                 _unitOfWork.Clinic.Update(DbClinic);
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
