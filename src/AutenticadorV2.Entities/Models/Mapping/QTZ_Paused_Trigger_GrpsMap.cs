using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class QTZ_Paused_Trigger_GrpsMap : EntityTypeConfiguration<QTZ_Paused_Trigger_Grps>
    {
        public QTZ_Paused_Trigger_GrpsMap()
        {
            // Primary Key
            this.HasKey(t => new { t.SCHED_NAME, t.TRIGGER_GROUP });

            // Properties
            this.Property(t => t.SCHED_NAME)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.TRIGGER_GROUP)
                .IsRequired()
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("QTZ_Paused_Trigger_Grps");
            this.Property(t => t.SCHED_NAME).HasColumnName("SCHED_NAME");
            this.Property(t => t.TRIGGER_GROUP).HasColumnName("TRIGGER_GROUP");
        }
    }
}
