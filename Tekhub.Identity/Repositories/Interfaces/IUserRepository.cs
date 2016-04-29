using System.Collections.Generic;
using Tekhub.Framework;
using Tekhub.Identity.Model;
using Tekhub.Identity.Model.Dto;

namespace Tekhub.Identity.Repositories.Interfaces
{
    public interface IUserRepository: IRepository<long, UserDto>
    {
        UserType GetUserType(int userTypeId);
        UserDto GetUserByEmail(string email);
        bool UserExists(string email);
        List<User> GetUsers(int userTypeId);

        void ChangeUserStatus(int userId, bool isActive);
    }
}
