using Microsoft.AspNetCore.Mvc;
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

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? SortBy = null, 
            bool IsAscending = true, [FromQuery] int PageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walks = dbContext.Walks
                .Include("difficulty")
                .Include("Region").AsQueryable();

            //Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }

                                    
            }

            // sorting
            if (string.IsNullOrWhiteSpace(SortBy) == false)
            {
                if (SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = IsAscending ? walks.OrderBy(x => x.Name): walks.OrderByDescending(x => x.Name);
                }
                else if (SortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = IsAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }
            //PageNation
            var skipResult = (PageNumber -1) * pageSize;


            return await walks.Skip(skipResult).Take(pageSize).ToListAsync();


            //return await dbContext.Walks
            //    .Include("difficulty")
            //    .Include("Region")
            //    .ToListAsync();
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
