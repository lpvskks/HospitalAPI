using webNET_2024_aspnet_1.DBContext.DTO.DictionaryDTO;

namespace webNET_2024_aspnet_1.Services.IServices
{
    public interface IDictionaryService
    {
        public Task<SpecialitiesPagedListDTO> GetPaginatedSpecialities(string name, int page, int size);
    }
}
