using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class QTZ_Simprop_TriggersMap : EntityTypeConfiguration<QTZ_Simprop_Triggers>
    {
        public QTZ_Simprop_TriggersMap()
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

            this.Property(t => t.STR_PROP_1)
                .HasMaxLength(512);

            this.Property(t => t.STR_PROP_2)
                .HasMaxLength(512);

            this.Property(t => t.STR_PROP_3)
                .HasMaxLength(512);

            // Table & Column Mappings
            this.ToTable("QTZ_Simprop_Triggers");
            this.Property(t => t.SCHED_NAME).HasColumnName("SCHED_NAME");
            this.Property(t => t.TRIGGER_NAME).HasColumnName("TRIGGER_NAME");
            this.Property(t => t.TRIGGER_GROUP).HasColumnName("TRIGGER_GROUP");
            this.Property(t => t.STR_PROP_1).HasColumnName("STR_PROP_1");
            this.Property(t => t.STR_PROP_2).HasColumnName("STR_PROP_2");
            this.Property(t => t.STR_PROP_3).HasColumnName("STR_PROP_3");
            this.Property(t => t.INT_PROP_1).HasColumnName("INT_PROP_1");
            this.Property(t => t.INT_PROP_2).HasColumnName("INT_PROP_2");
            this.Property(t => t.LONG_PROP_1).HasColumnName("LONG_PROP_1");
            this.Property(t => t.LONG_PROP_2).HasColumnName("LONG_PROP_2");
            this.Property(t => t.DEC_PROP_1).HasColumnName("DEC_PROP_1");
            this.Property(t => t.DEC_PROP_2).HasColumnName("DEC_PROP_2");
            this.Property(t => t.BOOL_PROP_1).HasColumnName("BOOL_PROP_1");
            this.Property(t => t.BOOL_PROP_2).HasColumnName("BOOL_PROP_2");

            // Relationships
            this.HasRequired(t => t.QTZ_Triggers)
                .WithOptional(t => t.QTZ_Simprop_Triggers);

        }
    }
}
