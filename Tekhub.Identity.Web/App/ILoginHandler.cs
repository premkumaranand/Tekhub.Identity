using System.Collections.Generic;
using Tekhub.Identity.Model.Dto;

namespace Tekhub.Identity.Web.App
{
    public interface ILoginHandler
    {
        void Login(string email, List<PermissionDto> userPermissions);
    }
}
