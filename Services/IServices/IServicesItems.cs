using DataAccessLayer.Models;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
   public interface IServicesItems
    {
        Task<IResponseDTO> InsertAsync(ItemsViewModel entity);
        IResponseDTO GetAll();
        Task<IResponseDTO> GetByIdAsync(long id);
        IResponseDTO Delete(ItemsViewModel model);
        IResponseDTO Update(ItemsViewModel entity);
    }
}
