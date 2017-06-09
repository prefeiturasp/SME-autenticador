using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class QTZ_Blob_TriggersMap : EntityTypeConfiguration<QTZ_Blob_Triggers>
    {
        public QTZ_Blob_TriggersMap()
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

            // Table & Column Mappings
            this.ToTable("QTZ_Blob_Triggers");
            this.Property(t => t.SCHED_NAME).HasColumnName("SCHED_NAME");
            this.Property(t => t.TRIGGER_NAME).HasColumnName("TRIGGER_NAME");
            this.Property(t => t.TRIGGER_GROUP).HasColumnName("TRIGGER_GROUP");
            this.Property(t => t.BLOB_DATA).HasColumnName("BLOB_DATA");
        }
    }
}
