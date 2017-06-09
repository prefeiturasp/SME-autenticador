using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class QTZ_CalendarsMap : EntityTypeConfiguration<QTZ_Calendars>
    {
        public QTZ_CalendarsMap()
        {
            // Primary Key
            this.HasKey(t => new { t.SCHED_NAME, t.CALENDAR_NAME });

            // Properties
            this.Property(t => t.SCHED_NAME)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.CALENDAR_NAME)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.CALENDAR)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("QTZ_Calendars");
            this.Property(t => t.SCHED_NAME).HasColumnName("SCHED_NAME");
            this.Property(t => t.CALENDAR_NAME).HasColumnName("CALENDAR_NAME");
            this.Property(t => t.CALENDAR).HasColumnName("CALENDAR");
        }
    }
}
