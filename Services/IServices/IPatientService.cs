using webNET_2024_aspnet_1.DBContext.DTO.InspectionDTO;
using webNET_2024_aspnet_1.DBContext.DTO.PatientDTO;
using webNET_2024_aspnet_1.DBContext.Models.Enums;

namespace webNET_2024_aspnet_1.Services.IServices
{
    public interface IPatientService
    {
        bool IsUniquePatient(string name);
        public Task<Guid> CreatePatient(PatientCreateDTO patientCreateDTO);
        public Task<PatientDTO> GetPatientCard(Guid id);
        public Task<List<InpectionWithoutNestedDTO>> GetInspectionsWithoutNested(Guid patientId, string? request);

        public Task<PatientPagedListDTO> GetPatientPagedList(Guid doctorId, string? name, List<Conclusion>? conslusions, Sorting? sorting,Boolean shudeledVisits,Boolean onlyMine, int page, int size);
    }
}
