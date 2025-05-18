using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domin_Model;

namespace NZWalks.API.Repository
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NzWalksDbContext dbContext;

        public SQLRegionRepository(NzWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync(); 
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null)
            {
                return null;
            }
            dbContext.Remove(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
            
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
           return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var exisitngRegion = await dbContext.Regions.FirstOrDefaultAsync(x =>x.Id == id);
            if (exisitngRegion == null)
            {
                return null;
            }
            exisitngRegion.Code = region.Code;
            exisitngRegion.Name = region.Name;
            exisitngRegion.RegionImageURL = region.RegionImageURL;

            await dbContext.SaveChangesAsync(); 
            return exisitngRegion;



        }
    }
}
