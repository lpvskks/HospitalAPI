using Microsoft.AspNetCore.Mvc;
using webNET_2024_aspnet_1.DBContext.DTO.InspectionDTO;

namespace webNET_2024_aspnet_1.Services.IServices
{
    public interface IInspectionService
    {
       public Task<Guid> CreateInspection(Guid patientId, Guid doctorId, InspectionCreateDTO inspectionCreateDTO);
        public Task EditInspection(Guid inspectionId, InspectionEditDTO inspectionEditDTO);
        public Task<InspectionDTO> GetConcreteInspection(Guid inspectionId);

        public Task<List<NestedInspectionDTO>> GetNestedInspections(Guid inspectionId);
    }
}
