using System.Collections.Generic;
using Tekhub.Identity.Model.Dto;

namespace Tekhub.Identity.Web.Common.App
{
    public interface ILoginHandler
    {
        void Login(string email, List<PermissionDto> userPermissions);
    }
}
