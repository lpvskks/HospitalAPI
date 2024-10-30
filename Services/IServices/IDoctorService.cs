using webNET_2024_aspnet_1.DBContext.DTO.DoctorDTO;
using webNET_2024_aspnet_1.DBContext.Models;

namespace webNET_2024_aspnet_1.Services.IServices
{
    public interface IDoctorService 
    {
        bool IsUniqueDoctor(string email);
        public Task<TokenResponseDTO> Register(DoctorRegisterDTO doctorRegisterDTO);
        public Task<TokenResponseDTO> Login(LoginCredentialsDTO loginCredentialsDTO);
        public Task Logout(string token);
        public Task<DoctorDTO> GetProfile(string token);
        public Task EditDoctorProfile(string token, DoctorEditDTO doctorEditDTO);
    }
}
