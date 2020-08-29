using ApplicationCore.IRepository;
using ApplicationCore.Repository;
using AutoMapper;

using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.Mapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Services
{
  public  class BaseServices
    {
        protected IUnitOfWork _unitOfWork;
        protected IResponseDTO _response;
        protected IMapper _mapper;
      //  protected readonly ApiContext _dbContext;
     //   private IConfiguration Configuration { get; }
        public BaseServices( IUnitOfWork unitOfWork, IResponseDTO responseDTO, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = responseDTO;
            //  Configuration = _Configuration;

            // var optioBuilder = new DbContextOptionsBuilder<ApiContext>();
            // optioBuilder.UseSqlServer(Configuration["ConnectionString:ClinicDB"]);
            // ApiContext apiContext = new ApiContext(optioBuilder.Options);
            //// _dbContext = apiContext;
            // this._unitOfWork = new UnitOfWork(apiContext);
            //var mapperConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new DomainProfile());
            //});

            //IMapper mapper = mapperConfig.CreateMapper();

        }
    }
}
