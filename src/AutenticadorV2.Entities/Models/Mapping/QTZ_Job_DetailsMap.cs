using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class QTZ_Job_DetailsMap : EntityTypeConfiguration<QTZ_Job_Details>
    {
        public QTZ_Job_DetailsMap()
        {
            // Primary Key
            this.HasKey(t => new { t.SCHED_NAME, t.JOB_NAME, t.JOB_GROUP });

            // Properties
            this.Property(t => t.SCHED_NAME)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.JOB_NAME)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.JOB_GROUP)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.DESCRIPTION)
                .HasMaxLength(250);

            this.Property(t => t.JOB_CLASS_NAME)
                .IsRequired()
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("QTZ_Job_Details");
            this.Property(t => t.SCHED_NAME).HasColumnName("SCHED_NAME");
            this.Property(t => t.JOB_NAME).HasColumnName("JOB_NAME");
            this.Property(t => t.JOB_GROUP).HasColumnName("JOB_GROUP");
            this.Property(t => t.DESCRIPTION).HasColumnName("DESCRIPTION");
            this.Property(t => t.JOB_CLASS_NAME).HasColumnName("JOB_CLASS_NAME");
            this.Property(t => t.IS_DURABLE).HasColumnName("IS_DURABLE");
            this.Property(t => t.IS_NONCONCURRENT).HasColumnName("IS_NONCONCURRENT");
            this.Property(t => t.IS_UPDATE_DATA).HasColumnName("IS_UPDATE_DATA");
            this.Property(t => t.REQUESTS_RECOVERY).HasColumnName("REQUESTS_RECOVERY");
            this.Property(t => t.JOB_DATA).HasColumnName("JOB_DATA");
        }
    }
}
