using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tekhub.Identity.Model.Dto;

namespace Tekhub.Identity.Web.Common.App
{
    public class UserPermissionsSessionManager : ICollectionSessionManager<PermissionDto>
    {
        private IList<PermissionDto> UserPermissions { get; set; }
        private readonly HttpSessionStateBase _session;
        private const string UserPermissionsSessionName = "UserPermissions";

        public UserPermissionsSessionManager(HttpSessionStateBase session)
        {
            _session = session;
            UserPermissions = _session[UserPermissionsSessionName] as IList<PermissionDto>;
        }

        public void Add(PermissionDto userClaim)
        {
            UserPermissions.Add(userClaim);
            _session[UserPermissionsSessionName] = UserPermissions;
        }

        public bool Contains(PermissionDto userClaim)
        {
            return UserPermissions.Any(uc => uc.Equals(userClaim));
        }

        public IList<PermissionDto> GetAll()
        {
            return UserPermissions;
        }


        public void AddRange(List<PermissionDto> userClaims)
        {
            if (UserPermissions == null)
            {
                UserPermissions = new List<PermissionDto>(userClaims);
            }
            else
            {
                ((List<PermissionDto>)UserPermissions).AddRange(userClaims);
            }

            _session[UserPermissionsSessionName] = UserPermissions;
        }

        public void Clear()
        {
            _session[UserPermissionsSessionName] = new List<PermissionDto>();
            UserPermissions = new List<PermissionDto>();
        }
    }
}