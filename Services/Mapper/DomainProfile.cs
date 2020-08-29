using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using DataAccessLayer.Models;
using Services.DTO;

namespace Services.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<ClinicViewModel, Clinic>().ReverseMap();
        }
    }
}
