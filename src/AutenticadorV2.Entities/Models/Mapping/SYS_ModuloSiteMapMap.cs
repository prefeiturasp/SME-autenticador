using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_ModuloSiteMapMap : EntityTypeConfiguration<SYS_ModuloSiteMap>
    {
        public SYS_ModuloSiteMapMap()
        {
            // Primary Key
            this.HasKey(t => new { t.sis_id, t.mod_id, t.msm_id });

            // Properties
            this.Property(t => t.sis_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.mod_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.msm_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.msm_nome)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.msm_descricao)
                .HasMaxLength(1000);

            this.Property(t => t.msm_url)
                .HasMaxLength(500);

            this.Property(t => t.msm_urlHelp)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("SYS_ModuloSiteMap");
            this.Property(t => t.sis_id).HasColumnName("sis_id");
            this.Property(t => t.mod_id).HasColumnName("mod_id");
            this.Property(t => t.msm_id).HasColumnName("msm_id");
            this.Property(t => t.msm_nome).HasColumnName("msm_nome");
            this.Property(t => t.msm_descricao).HasColumnName("msm_descricao");
            this.Property(t => t.msm_url).HasColumnName("msm_url");
            this.Property(t => t.msm_informacoes).HasColumnName("msm_informacoes");
            this.Property(t => t.msm_urlHelp).HasColumnName("msm_urlHelp");

            // Relationships
            this.HasRequired(t => t.SYS_Modulo)
                .WithMany(t => t.SYS_ModuloSiteMap)
                .HasForeignKey(d => new { d.sis_id, d.mod_id });

        }
    }
}
