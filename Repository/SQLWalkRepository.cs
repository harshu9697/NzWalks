using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domin_Model;

namespace NZWalks.API.Repository
{
    public class SQLWalkRepository : IwalkRepository
    {
        private readonly NzWalksDbContext dbContext;

        public SQLWalkRepository(NzWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var ExisitingWalk =await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (ExisitingWalk == null)
            {
                return null;
            }
            dbContext.Walks.Remove(ExisitingWalk);
            await dbContext.SaveChangesAsync();
            return ExisitingWalk;

        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await dbContext.Walks
                .Include("difficulty")
                .Include("Region")
                .ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks
                .Include("difficulty")
                .Include("Region")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var ExisitingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (ExisitingWalk == null)
            {
                return null; 
            }

            ExisitingWalk.Name = walk.Name;
            ExisitingWalk.Description = walk.Description;
            ExisitingWalk.LengthInKm = walk.LengthInKm;
            ExisitingWalk.WalkImageUrl = walk.WalkImageUrl;
            ExisitingWalk.DifficultyId = walk.DifficultyId;
            ExisitingWalk.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();
            return ExisitingWalk;
        }
    }
}
