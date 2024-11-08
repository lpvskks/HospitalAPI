using webNET_2024_aspnet_1.DBContext.DTO.DictionaryDTO;
using webNET_2024_aspnet_1.DBContext.DTO.PageDTO;

namespace webNET_2024_aspnet_1.DBContext.DTO.PatientDTO
{
    public class PatientPagedListDTO
    {
        public IEnumerable<PatientDTO> Patients { get; set; }
        public PageInfoDTO Pagination { get; set; }
    }
}
