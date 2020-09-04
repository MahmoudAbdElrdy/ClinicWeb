using ApplicationCore.Repository;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.IRepository
{
    public interface IUnitOfWork
    {
        int Commit();
        Task<int> CommitAsync();
        IGenericRepositry<Clinic> Clinic { get; }
        IGenericRepositry<Doctor> Doctor { get; }
        IGenericRepositry<Items> Items { get; }
        IGenericRepositry<Patient> Patient { get; }
        IGenericRepositry<Store> Store { get; }
    }
}
