using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Tekhub.Identity.Model;
using Tekhub.Identity.Repositories.Interfaces;

namespace Tekhub.Identity.Repositories
{
    public class PasswordResetRepository : IPasswordResetRepository
    {
        private readonly IUserManagementDbContext _context;

        public PasswordResetRepository(IUserManagementDbContext context)
        {
            _context = context;
        }

        public PasswordReset GetUserPasswordLinkByKey(long userId, string key)
        {
            return _context.PasswordLinks.SingleOrDefault(x => x.User.Id == userId && x.Key == key);
        }

        public PasswordReset GetPasswordLinkByKey(string key)
        {
            return _context.PasswordLinks.SingleOrDefault(x => x.Key == key);
        }

        public void Insert(PasswordReset pwdReset)
        {
            _context.PasswordLinks.Add(pwdReset);
            _context.SaveChanges();
        }

        public void Update(PasswordReset pwdReset)
        {
            _context.PasswordLinks.AddOrUpdate(pwdReset);
            _context.SaveChanges();
        }
    }
}
