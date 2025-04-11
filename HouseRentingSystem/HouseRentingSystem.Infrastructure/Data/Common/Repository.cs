
using Microsoft.EntityFrameworkCore;

namespace HouseRentingSystem.Infrastructure.Data.Common
{
    public class Repository : IRepository
    {
        private readonly DbContext context;

        public Repository(HouseRentingDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync<T>(T entity) where T : class
             => await DbSet<T>().AddAsync(entity);

        public IQueryable<T> All<T>() where T : class
            => DbSet<T>();

        public IQueryable<T> AllAsReadOnly<T>() where T : class
            => DbSet<T>()
                .AsNoTracking();

        public void Remove<T>(T entity) where T : class
            => DbSet<T>().Remove(entity);

        public async Task<int> SaveChangesAsync()
            => await context.SaveChangesAsync();

        private DbSet<T> DbSet<T>() where T : class
            => context.Set<T>();
    }
}
