using System.Collections.Generic;

namespace Tekhub.Identity.Model
{
    public class UserType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
