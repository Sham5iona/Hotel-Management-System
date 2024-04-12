using HMS.Areas.Identity.Model;
using HMS.DTOs;

namespace HMS.Services
{
    public interface IAdminService
    {
        public Task<bool> UpdateAdminAsync(AdminDTO AdminDTO);
        public Task<bool> DeleteAdminAsync(string id);
        public IEnumerable<Admin> GetAdmins();
        public Task<string> GetAdminIDAsync(AdminDTO AdminDTO);

        public Task<bool> CheckIfAlreadyExistsAsync(AdminDTO AdminDTO);

        public Task<Admin> GetAdminByIdAsync(string id);
    }
}
