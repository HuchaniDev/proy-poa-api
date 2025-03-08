using Proy.Domain.Models.Authentication;
using Proy.Domain.Repositories.Common;

namespace Proy.Domain.Repositories.Authentication;

public interface IUserRepository: IGenericRepository<UserModel>
{
    Task<bool>IsUsedEmail(string email);
    Task<bool>IsUsedUsername(string username);
    
}