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
    public class ServicesPatient : BaseServices,IServicesPatient
    {
       
        
        public ServicesPatient(IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
            : base(unitOfWork,responseDTO,mapper)
        {
           
           
        }
       

        public IResponseDTO GetAll()
        {
        
            try
            {
                var result = _unitOfWork.Patient.GetAll();

                var resultList = _mapper.Map<List<PatientViewModel>>(result);
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

        
     public   IResponseDTO Delete(PatientViewModel model)
        {
            try
            {
                var DBmodel = _mapper.Map<Patient>(model);
                var entityEntry = _unitOfWork.Patient.Remove(DBmodel);

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
                var Dbmodel =await _unitOfWork.Patient.GetByIdAsync(id);

                var PatientViewModel = _mapper.Map<PatientViewModel>(Dbmodel);
                _response.Data = PatientViewModel;
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

      public async Task<IResponseDTO> InsertAsync(PatientViewModel entity)
        {
            try
            {
                var ViewModel = _mapper.Map<Patient>(entity);
                var Dbmodel =await _unitOfWork.Patient.InsertAsync(ViewModel);
               
                int save = _unitOfWork.Commit();

                if (save == 200)
                {
                    var PatientViewModel = _mapper.Map<PatientViewModel>(Dbmodel);
                    _response.Data = PatientViewModel;
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

      public  IResponseDTO Update(PatientViewModel entity)
        {
            try
            {
                var DbPatient= _mapper.Map<Patient>(entity);
                 _unitOfWork.Patient.Update(DbPatient);
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
