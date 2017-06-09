using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_VisaoModuloMenuMap : EntityTypeConfiguration<SYS_VisaoModuloMenu>
    {
        public SYS_VisaoModuloMenuMap()
        {
            // Primary Key
            this.HasKey(t => new { t.vis_id, t.sis_id, t.mod_id, t.msm_id });

            // Properties
            this.Property(t => t.vis_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.sis_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.mod_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.msm_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("SYS_VisaoModuloMenu");
            this.Property(t => t.vis_id).HasColumnName("vis_id");
            this.Property(t => t.sis_id).HasColumnName("sis_id");
            this.Property(t => t.mod_id).HasColumnName("mod_id");
            this.Property(t => t.msm_id).HasColumnName("msm_id");
            this.Property(t => t.vmm_ordem).HasColumnName("vmm_ordem");

            // Relationships
            this.HasRequired(t => t.SYS_ModuloSiteMap)
                .WithMany(t => t.SYS_VisaoModuloMenu)
                .HasForeignKey(d => new { d.sis_id, d.mod_id, d.msm_id });
            this.HasRequired(t => t.SYS_VisaoModulo)
                .WithMany(t => t.SYS_VisaoModuloMenu)
                .HasForeignKey(d => new { d.vis_id, d.sis_id, d.mod_id });

        }
    }
}
