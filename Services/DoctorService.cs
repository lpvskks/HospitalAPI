using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using webNET_2024_aspnet_1.Additional_Services.TokenHelpers;
using webNET_2024_aspnet_1.AdditionalServices.Exceptions;
using webNET_2024_aspnet_1.AdditionalServices.HashPassword;
using webNET_2024_aspnet_1.AdditionalServices.Validators;
using webNET_2024_aspnet_1.DBContext;
using webNET_2024_aspnet_1.DBContext.DTO.DoctorDTO;
using webNET_2024_aspnet_1.DBContext.Models;
using webNET_2024_aspnet_1.DBContext.Models.Enums;
using webNET_2024_aspnet_1.Services.IServices;

namespace webNET_2024_aspnet_1.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly AppDBContext _dbContext;
        private readonly TokenInteraction _tokenHelper;
        public DoctorService(AppDBContext dbContext, TokenInteraction tokenHelper)
        {
            _dbContext = dbContext;
            _tokenHelper = tokenHelper;
        }
        public bool IsUniqueDoctor(string email)
        {
            var doctor = _dbContext.Doctors.FirstOrDefault(x => x.Email == email);
            if (doctor == null)
            {
                return true;
            }
            return false;
        }


        public async Task<TokenResponseDTO> Register(DoctorRegisterDTO doctorRegisterDTO)
        {
            if (!IsUniqueDoctor(doctorRegisterDTO.Email))
            {
                throw new BadRequestException("Email уже используется");
            }

            if (!NameValidator.IsValidName(doctorRegisterDTO.Name))
            {
                throw new BadRequestException("Неправильный формат имени. Имя и фамилия должны начинаться с заглавной буквы. Допускаются только буквы и тире. Отчество является необязательным.");
            }

            if (!EmailValidator.ValidateEmail(doctorRegisterDTO.Email))
            {
                throw new BadRequestException("Неверный формат email!");
            }

            if (!PhoneNumberVlidator.IsValidePhoneNumber(doctorRegisterDTO.Phone))
            {
                throw new BadRequestException("Неверный формат номера телефона!");
            }

            if (!BirthdayValidator.ValidateBirthday(doctorRegisterDTO.Birthday))
            {
                throw new BadRequestException("Дата рождения должна быть в пределах 01.01.1900 и не позднее нынешнего времени.");
            }

            if (!PasswordValidator.IsValidPassword(doctorRegisterDTO.Password))
            {
                throw new BadRequestException("Пароль должен содержать минимум 8 символов, включая хотя бы одну заглавную букву, одну строчную букву, одну цифру и один специальный символ.");
            }

           

            Doctor doctor = new Doctor()
            {
                Id = Guid.NewGuid(),
                Name = doctorRegisterDTO.Name,
                Password = HashPassword.HashingPassword(doctorRegisterDTO.Password),  
                Email = doctorRegisterDTO.Email,
                Birthday = doctorRegisterDTO.Birthday,
                Gender = doctorRegisterDTO.Gender,
                Phone = doctorRegisterDTO.Phone,
                CreateTime = DateTime.UtcNow
            };
            await _dbContext.Doctors.AddAsync(doctor); 
            await _dbContext.SaveChangesAsync();

            var token = _tokenHelper.GenerateToken(doctor);

            return new TokenResponseDTO
            {
                Token = token
            };

        }
        public async Task<TokenResponseDTO> Login(LoginCredentialsDTO loginCredentialsDTO)
        {
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(d => d.Email == loginCredentialsDTO.Email);
            if (doctor == null)
            {
                throw new BadRequestException("Неправильный Email или пароль");
            }
            else if (!HashPassword.VerifyPassword(loginCredentialsDTO.Password, doctor.Password))
            {
                throw new BadRequestException("Неправильный Email или пароль");
            }

            var token = _tokenHelper.GenerateToken(doctor);
            return new TokenResponseDTO
            {
                Token = token
            };

        }

        public async Task Logout(string token)
        {
            string id = _tokenHelper.GetIdFromToken(token);

            if (Guid.TryParse(id, out Guid doctorId) && doctorId != Guid.Empty)
            {
                await _dbContext.BlackTokens.AddAsync(new BlackToken { Blacktoken = token });
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new BadRequestException("Некорректный ID: не удалось извлечь или преобразовать id из токена.");
            }
        }

        public async Task<DoctorDTO> GetProfile(string token)
        {
            string doctorId = _tokenHelper.GetIdFromToken(token);
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(d => d.Id == Guid.Parse(doctorId));
            if (doctor != null)
            {
                return new DoctorDTO
                {
                    Id = doctor.Id,
                    CreateTime = doctor.CreateTime,
                    Name = doctor.Name,
                    Birthday = doctor.Birthday,
                    Gender = doctor.Gender,
                    Email = doctor.Email,
                    Phone = doctor.Phone
                };
            }
            else
            {
                throw new UnauthorizedException("Пользователь не авторизован");
            }

        }

        public async Task EditDoctorProfile(string token, DoctorEditDTO doctorEditDTO)
        {
            string doctorId = _tokenHelper.GetIdFromToken(token);

            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(d => d.Id == Guid.Parse(doctorId));
            if (doctor != null)
            {
                if (doctorEditDTO.Name != null) {
                    if (!NameValidator.IsValidName(doctorEditDTO.Name))
                    {
                        throw new BadRequestException("Неправильный формат имени. Имя и фамилия должны начинаться с заглавной буквы. Допускаются только буквы и тире. Отчество является необязательным.");
                    }

                    doctor.Name = doctorEditDTO.Name;
                }

                if (doctorEditDTO.Phone != null) {
                    if (!PhoneNumberVlidator.IsValidePhoneNumber(doctorEditDTO.Phone))
                    {
                        throw new BadRequestException("Неверный формат номера телефона!");
                    }
                    doctor.Phone = doctorEditDTO.Phone;
                }

                if (doctorEditDTO.Email != null)
                {
                    if (!EmailValidator.ValidateEmail(doctorEditDTO.Email))
                    {
                        throw new BadRequestException("Неверный формат email!");
                    }
                    doctor.Email = doctorEditDTO.Email;
                }

                if (doctorEditDTO.Birthday != null)
                {
                    if (!BirthdayValidator.ValidateBirthday((DateTime)(doctorEditDTO.Birthday)))
                    {
                        throw new BadRequestException("Дата рождения должна быть в пределах 01.01.1900 и не позднее нынешнего времени.");
                    }
                    doctor.Birthday = (DateTime)(doctorEditDTO?.Birthday);
                }
                if (doctorEditDTO.Gender != null) {
                    doctor.Gender = doctorEditDTO.Gender;
                }

                await _dbContext.SaveChangesAsync();
            }

            else
            {
                throw new UnauthorizedException("Пользователь не авторизован");
            }
        }
    }
}
