using Jibit.Domain.Aggregates;
using Jibit.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Jibit.Infra.Repositories
{
    public class RequestRepository
    {
        private readonly AppDbContext _dbContext;

        public RequestRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddRequestAsync(Request request)
        {
            _dbContext.Request.Add(request);
            await _dbContext.SaveChangesAsync();
        }
    }
}