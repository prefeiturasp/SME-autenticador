using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class QTZ_LocksMap : EntityTypeConfiguration<QTZ_Locks>
    {
        public QTZ_LocksMap()
        {
            // Primary Key
            this.HasKey(t => new { t.SCHED_NAME, t.LOCK_NAME });

            // Properties
            this.Property(t => t.SCHED_NAME)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.LOCK_NAME)
                .IsRequired()
                .HasMaxLength(40);

            // Table & Column Mappings
            this.ToTable("QTZ_Locks");
            this.Property(t => t.SCHED_NAME).HasColumnName("SCHED_NAME");
            this.Property(t => t.LOCK_NAME).HasColumnName("LOCK_NAME");
        }
    }
}
