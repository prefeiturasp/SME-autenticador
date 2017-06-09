using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_VisaoModuloMap : EntityTypeConfiguration<SYS_VisaoModulo>
    {
        public SYS_VisaoModuloMap()
        {
            // Primary Key
            this.HasKey(t => new { t.vis_id, t.sis_id, t.mod_id });

            // Properties
            this.Property(t => t.vis_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.sis_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.mod_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SYS_VisaoModulo");
            this.Property(t => t.vis_id).HasColumnName("vis_id");
            this.Property(t => t.sis_id).HasColumnName("sis_id");
            this.Property(t => t.mod_id).HasColumnName("mod_id");

            // Relationships
            this.HasRequired(t => t.SYS_Modulo)
                .WithMany(t => t.SYS_VisaoModulo)
                .HasForeignKey(d => new { d.sis_id, d.mod_id });
            this.HasRequired(t => t.SYS_Visao)
                .WithMany(t => t.SYS_VisaoModulo)
                .HasForeignKey(d => d.vis_id);

        }
    }
}
