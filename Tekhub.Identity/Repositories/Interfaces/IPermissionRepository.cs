using System.Collections.Generic;
using Tekhub.Framework;
using Tekhub.Identity.Model;
using Tekhub.Identity.Model.Dto;

namespace Tekhub.Identity.Repositories.Interfaces
{
    public interface IPermissionRepository: IRepository<int, PermissionDto>
    {
        List<PermissionDto> GetByUserType(UserType userType);
        void AddUserPermissions(long userId, UserType userType);
        List<PermissionDto> Get();
        List<Permission> Get(List<int> permissionIds);
    }
}
