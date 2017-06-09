using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class QTZ_Cron_TriggersMap : EntityTypeConfiguration<QTZ_Cron_Triggers>
    {
        public QTZ_Cron_TriggersMap()
        {
            // Primary Key
            this.HasKey(t => new { t.SCHED_NAME, t.TRIGGER_NAME, t.TRIGGER_GROUP });

            // Properties
            this.Property(t => t.SCHED_NAME)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.TRIGGER_NAME)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.TRIGGER_GROUP)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.CRON_EXPRESSION)
                .IsRequired()
                .HasMaxLength(120);

            this.Property(t => t.TIME_ZONE_ID)
                .HasMaxLength(80);

            // Table & Column Mappings
            this.ToTable("QTZ_Cron_Triggers");
            this.Property(t => t.SCHED_NAME).HasColumnName("SCHED_NAME");
            this.Property(t => t.TRIGGER_NAME).HasColumnName("TRIGGER_NAME");
            this.Property(t => t.TRIGGER_GROUP).HasColumnName("TRIGGER_GROUP");
            this.Property(t => t.CRON_EXPRESSION).HasColumnName("CRON_EXPRESSION");
            this.Property(t => t.TIME_ZONE_ID).HasColumnName("TIME_ZONE_ID");

            // Relationships
            this.HasRequired(t => t.QTZ_Triggers)
                .WithOptional(t => t.QTZ_Cron_Triggers);

        }
    }
}
