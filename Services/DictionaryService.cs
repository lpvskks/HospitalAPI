using System.Collections;
using Microsoft.EntityFrameworkCore;
using webNET_2024_aspnet_1.DBContext;
using webNET_2024_aspnet_1.DBContext.DTO.DictionaryDTO;
using webNET_2024_aspnet_1.DBContext.DTO.PageDTO;
using webNET_2024_aspnet_1.Migrations;
using webNET_2024_aspnet_1.Services.IServices;

namespace webNET_2024_aspnet_1.Services
{
    public class DictionaryService : IDictionaryService
    {
        private readonly AppDBContext _dbContext;

        public DictionaryService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SpecialitiesPagedListDTO> GetPaginatedSpecialities(string name, int page, int size)
        {
            var query = _dbContext.Specialties.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(s => s.Name.Contains(name));
            }
            query = query.OrderBy(s => s.Name);
            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / size);

            var specialities = await query.Skip((page - 1) * size).Take(size).Select(s => new SpecialityDTO
            {
                Id = s.Id,
                Name = s.Name,
                CreateTime = s.CreateTime
            }).ToListAsync();

            return new SpecialitiesPagedListDTO
            {
                Specialities = specialities,
                Pagination = new PageInfoDTO
                {
                    Size = size,
                    Count = totalPages,
                    Current = page
                }
                
            };
        }
    }
}
