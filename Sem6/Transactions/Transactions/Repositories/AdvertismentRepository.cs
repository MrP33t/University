using Microsoft.EntityFrameworkCore;
using Transactions.Data;
using Transactions.Models;

namespace Transactions.Repositories
{
    public class AdvertismentRepository : IAdvertismentRepository
    {
        private readonly DataContext dbContext;
        public AdvertismentRepository(DataContext context)
        {
            dbContext = context;
        }
        public async Task Create(Advertisment advertisment)
        {
            var addTransaction = dbContext.Database.BeginTransaction();

            try
            {
                await dbContext.Advertisments.AddAsync(advertisment);
                await dbContext.SaveChangesAsync();
                addTransaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                addTransaction.Rollback();
            }

            addTransaction.Dispose();
        }

        public async Task Delete(int id)
        {
            var deleteTransaction = dbContext.Database.BeginTransaction();

            var advertDB = await Get(id);
            if (advertDB is null)
                return;

            try
            {
                dbContext.Advertisments.Remove(advertDB);
                await dbContext.SaveChangesAsync();
                deleteTransaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                deleteTransaction.Rollback();
            }

            deleteTransaction.Dispose();
        }

        public async Task<List<Advertisment>> Get()
        {
            return await dbContext.Advertisments.ToListAsync();
        }

        public async Task<Advertisment?> Get(int id)
        {
            return await dbContext.Advertisments.FindAsync(id);
        }

        public async Task Update(Advertisment advertisment)
        {
            var updateTransaction = dbContext.Database.BeginTransaction();
            var advertDB = await Get(advertisment.Id);
            if (advertDB is null)
                return;
            try
            {
                advertDB.Name = advertisment.Name;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                updateTransaction.Rollback();
            }
            updateTransaction.CreateSavepoint("save1");
            try
            {
                advertDB.ImageURL = advertisment.ImageURL;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                updateTransaction.RollbackToSavepoint("save1");
            }
            await dbContext.SaveChangesAsync();
            updateTransaction.Commit();
            updateTransaction.Dispose();
        }
    }
}
