using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekhub.Identity.Model;

namespace Tekhub.Identity.Repositories.ModelBuilders
{
    internal class PasswordResetModelBuilder
    {
        public static void Build(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PasswordReset>().Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<PasswordReset>().Property(t => t.ActivatedOn).IsOptional().HasColumnType("datetime2");
            modelBuilder.Entity<PasswordReset>().Property(t => t.RequestedOn).HasColumnType("datetime2");
            modelBuilder.Entity<PasswordReset>().Property(t => t.ExpireOn).HasColumnType("datetime2");
            modelBuilder.Entity<PasswordReset>().Property(t => t.Key).HasMaxLength(32);
            modelBuilder.Entity<PasswordReset>().Property(t => t.RequestedIp).HasMaxLength(45);
        }
    }
}
