using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class QTZ_Scheduler_StateMap : EntityTypeConfiguration<QTZ_Scheduler_State>
    {
        public QTZ_Scheduler_StateMap()
        {
            // Primary Key
            this.HasKey(t => new { t.SCHED_NAME, t.INSTANCE_NAME });

            // Properties
            this.Property(t => t.SCHED_NAME)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.INSTANCE_NAME)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("QTZ_Scheduler_State");
            this.Property(t => t.SCHED_NAME).HasColumnName("SCHED_NAME");
            this.Property(t => t.INSTANCE_NAME).HasColumnName("INSTANCE_NAME");
            this.Property(t => t.LAST_CHECKIN_TIME).HasColumnName("LAST_CHECKIN_TIME");
            this.Property(t => t.CHECKIN_INTERVAL).HasColumnName("CHECKIN_INTERVAL");
        }
    }
}
