using webNET_2024_aspnet_1.DBContext.DTO.PageDTO;

namespace webNET_2024_aspnet_1.DBContext.DTO.DictionaryDTO
{
    public class IcdTenSearchDTO
    {
        public IEnumerable<IcdTenRecordDTO> Records { get; set; }
        public PageInfoDTO Pagination { get; set; }
    }
}
