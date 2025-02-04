﻿using System.Collections;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using webNET_2024_aspnet_1.DBContext;
using webNET_2024_aspnet_1.DBContext.DTO.DictionaryDTO;
using webNET_2024_aspnet_1.DBContext.DTO.PageDTO;
using webNET_2024_aspnet_1.DBContext.Models;

using System.Text.Json;
using webNET_2024_aspnet_1.Services.IServices;
using System.Data.Common;
using Newtonsoft.Json;

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
            if (page <= 0) page = 1;
            if (size <= 0) size = 1;
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

        public async Task AddDataIcd(string filePath)
        {
            if (await _dbContext.IcdTens.AnyAsync())
            {
                return;
            }
            var jsonData = await File.ReadAllTextAsync(filePath);
            var records = JsonConvert.DeserializeObject<List<IcdTenDTO>>(jsonData); 

            var icdTens = records.Select(record => new IcdTen
            {
                Id = Guid.NewGuid(),
                UnicalId = record.UnicalId,
                RecordCode = record.RecordCode,
                Code = record.Code,
                Name = record.Name,
                ParentId = record.ParentId,
                Actual = record.Actual,
                CreateTime = DateTime.UtcNow
            }).ToList();


            await _dbContext.IcdTens.AddRangeAsync(icdTens);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IcdTenSearchDTO> SearchIcdTenRecords(string request, int page, int size)
        {
            if (page <= 0) page = 1;
            if (size <= 0) size = 1;
            var query = _dbContext.IcdTens.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request))
            {
                query = query.Where(r => r.Code.Contains(request) || r.Name.Contains(request));
            }

            query = query.OrderBy(r => r.Code != request).ThenBy(r => r.Code).ThenBy(r => r.Name);           

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / size);

            var records = await query.Skip((page - 1) * size).Take(size).Select(r => new IcdTenRecordDTO 
            {
                Id = r.Id,
                CreateTime = r.CreateTime,
                Code = r.Code,
                Name = r.Name
            }).ToListAsync();

            return new IcdTenSearchDTO
            {
                Records = records,
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
