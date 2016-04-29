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
    internal class UserTypeModelBuilder
    {
        public static void Build(DbModelBuilder modelBuilder)
        {
            var permissionEntity = modelBuilder.Entity<UserType>();
            permissionEntity.Property(uc => uc.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            permissionEntity.Property(uc => uc.Name).HasMaxLength(100);
        }
    }
}
