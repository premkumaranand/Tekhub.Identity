using System;
using System.Data.Entity;
using Tekhub.Identity.Model;

namespace Tekhub.Identity.Repositories.Interfaces
{
    public interface IUserManagementDbContext:IDisposable
    {
        DbSet<User> Users { get; set; }
        DbSet<Permission> Permissions { get; set; }
        DbSet<PasswordReset> PasswordLinks { get; set; }
        DbSet<UserType> UserTypes { get; set; }
        int SaveChanges();
    }
}
