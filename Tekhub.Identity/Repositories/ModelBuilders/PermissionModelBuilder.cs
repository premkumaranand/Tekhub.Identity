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
    internal class PermissionModelBuilder
    {
        public static void Build(DbModelBuilder modelBuilder)
        {
            var permissionEntity = modelBuilder.Entity<Permission>();
            permissionEntity.Property(uc => uc.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            permissionEntity.Property(uc => uc.Controller).HasMaxLength(100);
            permissionEntity.Property(uc => uc.Action).HasMaxLength(100);
            permissionEntity.Property(uc => uc.Area).HasMaxLength(100);
            permissionEntity.Property(uc => uc.ActionType).HasMaxLength(100);
        }
    }
}
