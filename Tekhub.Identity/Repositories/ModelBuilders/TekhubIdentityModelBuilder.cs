using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tekhub.Identity.Repositories.ModelBuilders
{
    public class TekhubIdentityModelBuilder
    {
        public static void Build(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

            var builders =
                Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(t => t.FullName.EndsWith("ModelBuilder") && t != typeof(TekhubIdentityModelBuilder));

            foreach (var builder in builders)
            {
                builder.GetMethod("Build").Invoke(null, new object[] { modelBuilder });
            }
        }
    }
}
