using DataAccessLayer.Models;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
   public interface IServicesDoctor
    {
        Task<IResponseDTO> InsertAsync(DoctorViewModel entity);
       IResponseDTO GetAll();
        Task<IResponseDTO> GetByIdAsync(long id);
        IResponseDTO Delete(DoctorViewModel model);
        IResponseDTO Update(DoctorViewModel entity);
    }
}
