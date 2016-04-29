using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekhub.Identity.Model;

namespace Tekhub.Identity.Repositories.ModelBuilders
{
    internal class UserModelBuilder
    {
        public static void Build(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.Name).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<User>().Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(
                        new IndexAttribute("IX_Email") { IsUnique = true }));

            modelBuilder.Entity<User>().Property(u => u.Password).IsOptional().HasMaxLength(100);
            modelBuilder.Entity<User>().Property(u => u.RegistrationStatus).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.PhoneNumber).HasMaxLength(15).IsRequired();
        }
    }
}
