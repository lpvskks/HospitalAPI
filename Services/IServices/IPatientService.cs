using webNET_2024_aspnet_1.DBContext.DTO.PatientDTO;

namespace webNET_2024_aspnet_1.Services.IServices
{
    public interface IPatientService
    {
        bool IsUniquePatient(string name);
        public Task<Guid> CreatePatient(PatientCreateDTO patientCreateDTO);
        public Task<PatientDTO> GetPatientCard(Guid id);
    }
}
