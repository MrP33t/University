using Transactions.Models;

namespace Transactions.Repositories
{
    public interface IAdvertismentRepository
    {
        public Task<List<Advertisment>> Get();
        public Task<Advertisment?> Get(int id);
        public Task Create(Advertisment advertisment);
        public Task Update(Advertisment advertisment);
        public Task Delete(int id);
    }
}
