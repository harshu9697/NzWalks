using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domin_Model;

namespace NZWalks.API.Repository
{
    public interface IwalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? filterOn = null,string? filterQuery = null, string? SortBy = null, bool IsAScending = true,
            [FromQuery] int PageNumber = 1, [FromQuery] int pageSize = 1000);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk?> UpdateAsync(Guid id, Walk walk);
        Task<Walk?> DeleteAsync(Guid id);
    }
}
