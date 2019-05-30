using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using villaayab.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Services
{
    public class CustomUnitOfWork : IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        public CustomUnitOfWork(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        // Blog
        private CustomGenericRepository<Blog> _blogRepository;
        public CustomGenericRepository<Blog> BlogRepository => _blogRepository ?? (_blogRepository = new CustomGenericRepository<Blog>(_dbContext));

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}

// https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application