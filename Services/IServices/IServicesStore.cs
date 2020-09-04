using DataAccessLayer.Models;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
   public interface IServicesStore
    {
        Task<IResponseDTO> InsertAsync(StoreViewModel entity);
       IResponseDTO GetAll();
        Task<IResponseDTO> GetByIdAsync(long id);
        IResponseDTO Delete(StoreViewModel model);
        IResponseDTO Update(StoreViewModel entity);
    }
}
