using DataAccessLayer.Models;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
   public interface IServicesPatient
    {
        Task<IResponseDTO> InsertAsync(PatientViewModel entity);
       IResponseDTO GetAll();
        Task<IResponseDTO> GetByIdAsync(long id);
        IResponseDTO Delete(PatientViewModel model);
        IResponseDTO Update(PatientViewModel entity);
    }
}
