using Proy.Domain.Dtos.Authentication;
using Proy.Domain.Models.Authentication;
using Proy.Domain.Repositories.Common;

namespace Proy.Domain.Repositories.Authentication;

public interface IUserRepository: IGenericRepository<UserModel>
{
    Task<List<UserDetailDto>>GetAllAsync();
    Task<UserModel?>GetByEmail(string email);
    Task<UserModel?>GetByUsername(string username);
    Task<List<string>>GetRolesAsync(int UserId);
    Task<bool>ChangeStatusActiveAsync(int id, bool status);
    Task<bool>ChangePasswordAsync(int id, string newPassword);
    
}