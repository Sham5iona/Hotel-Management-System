using AutoMapper;
using HMS.Areas.Identity.Model;
using HMS.Data;
using HMS.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HMS.Services
{
    public class AdminService : IAdminService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AdminService> _logger;
        private static string _admin_id;
        private readonly UserManager<Admin> _userManager;
        private readonly SignInManager<Admin> _signInManager;
        public AdminService(IMapper mapper,
            ILogger<AdminService> logger,
            UserManager<Admin> userManager,
            SignInManager<Admin> signInManager)
        {
            this._mapper = mapper;
            this._logger = logger;
            this._signInManager = signInManager;
            this._userManager = userManager;
        }
        

        public async Task<bool> CheckIfAlreadyExistsAsync(AdminDTO AdminDTO)
        {
            var admins = GetAdmins();

            bool user_already_exists = admins.Any(a =>
                a.UserName == AdminDTO.Username &&
                a.Id != _admin_id);

            if (user_already_exists)
            {
                AdminDTO.AlreadyExists = "Username is already taken!" +
                    "Try other one!";

                return true; // admin exists
            }

            //admin doesn't exist
            return false;

        }

        public async Task<bool> DeleteAdminAsync(string id)
        {
            try
            {
                var admin = await _userManager.FindByIdAsync(id);
                var result = await _userManager.DeleteAsync(admin);

                if(result.Succeeded)
                {
                    return true;
                }

                throw new Exception();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<Admin> GetAdminByIdAsync(string id)
        {
            var admin = await _userManager.FindByIdAsync(id);

            _admin_id = id;

            return admin;
        }

        public async Task<string> GetAdminIDAsync(AdminDTO AdminDTO)
        {
            try
            {
                _mapper.Map<Admin>(AdminDTO);

                Admin? wanted_admin = await GetAdminByIdAsync(_admin_id);

                if (wanted_admin == null)
                {
                    throw new Exception("There is no admin with this data!");

                }

                return wanted_admin.Id;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return string.Empty;
            }
        }

        public IEnumerable<Admin> GetAdmins()
        {
            try
            {

                var admins = _userManager.Users;

                return admins;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateAdminAsync(AdminDTO AdminDTO)
        {
            try
            {

                if (!(await CheckIfAlreadyExistsAsync(AdminDTO)))
                {

                    Admin admin = await GetAdminByIdAsync(_admin_id);

                    _mapper.Map(AdminDTO, admin);
                    string token = await _userManager
                                .GeneratePasswordResetTokenAsync(admin);

                    var result = await _userManager.ResetPasswordAsync(
                                       admin, token, AdminDTO.Password);

                    if (result.Succeeded)
                    {

                        await _signInManager.RefreshSignInAsync(admin);
                            return true;
                    }

                    _logger.LogError(result.Errors.First().Description);
                }

                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }

        }
    }
}
