using DataAccessLayer.Models;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
   public interface IServicesClinic
    {
        Task<IResponseDTO> InsertAsync(ClinicViewModel entity);
       IResponseDTO GetAll();
        Task<IResponseDTO> GetByIdAsync(long id);
        IResponseDTO Delete(ClinicViewModel model);
        IResponseDTO Update(ClinicViewModel entity);
    }
}
