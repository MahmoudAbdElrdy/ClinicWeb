using ApplicationCore.IRepository;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApiContext _dbContext;

        private bool disposed = false;
     //   private IConfiguration _Configuration { get; }
        public UnitOfWork(ApiContext dbContext)
        {
            _dbContext = dbContext;
            //_Configuration = Configuration;
        }

        //public int Commit()
        //{
        //    return _dbContext.SaveChanges();
        //}
        public int Commit()
        {
            int returnValue = 200;
            using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
                //  {
                try
                {
                    _dbContext.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.GetBaseException() as SqlException;

                    if (sqlException != null)
                    {
                        var number = sqlException.Number;

                        if (number == 547)
                        {
                            returnValue = 501;

                        }
                        else
                            returnValue = 500;
                    }
                }
                catch (Exception ex)
                {
                    //Log Exception Handling message                      
                    returnValue = 500;
                    dbContextTransaction.Rollback();
                }
            //    }

            return returnValue;
        }
        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Clinic
      
        public IGenericRepositry<Clinic> Clinic
        {
            get
            {
                return new GenericRepository<Clinic>(_dbContext);

            }
        }

       // IGenericRepositry<Clinic> IUnitOfWork.Clinic => throw new NotImplementedException();
    }
}
