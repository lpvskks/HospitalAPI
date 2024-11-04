using webNET_2024_aspnet_1.DBContext.DTO.PageDTO;

namespace webNET_2024_aspnet_1.DBContext.DTO.DictionaryDTO
{
    public class SpecialitiesPagedListDTO
    {
        public IEnumerable<SpecialityDTO> Specialities { get; set; }
        public PageInfoDTO Pagination {  get; set; }
    }
}
