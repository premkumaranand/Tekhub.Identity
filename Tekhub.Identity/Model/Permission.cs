using System;
using System.Collections.Generic;

namespace Tekhub.Identity.Model
{
    public partial class Permission
    {
        public int Id { get; set; }
        public UserType UserType { get; set; }
        public string Domain { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Area { get; set; }
        public string ActionType { get; set; }

        public string GetUrl()
        {
            string url = string.IsNullOrEmpty(Area) || Area.Equals("Root", StringComparison.CurrentCultureIgnoreCase) ? Controller : String.Format("{0}/{1}", Area, Controller);

            if (string.IsNullOrEmpty(Action))
            {
                url = String.Format("/{0}/", url);
            }
            else
            {
                url = String.Format("/{0}/{1}/", url, Action);
            }

            return url;
        }

        public virtual List<User> Users { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (this.GetType() != obj.GetType()) return false;

            var otherPermission = (Permission)obj;

            if (!otherPermission.Area.Equals(this.Area, StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            if (!otherPermission.Domain.Equals(this.Domain, StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            if (string.IsNullOrEmpty(ActionType))
            {
                if (string.IsNullOrEmpty(Action))
                {
                    return Controller.Equals(otherPermission.Controller, StringComparison.InvariantCultureIgnoreCase);
                }

                return Controller.Equals(otherPermission.Controller, StringComparison.InvariantCultureIgnoreCase) &&
                       Action.Equals(otherPermission.Action, StringComparison.InvariantCultureIgnoreCase);
            }

            return Controller.Equals(otherPermission.Controller, StringComparison.InvariantCultureIgnoreCase) &&
                   Action.Equals(otherPermission.Action, StringComparison.InvariantCultureIgnoreCase) &&
                   ActionType.Equals(otherPermission.ActionType, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
