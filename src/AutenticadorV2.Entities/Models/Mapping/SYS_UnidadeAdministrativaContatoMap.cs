using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_UnidadeAdministrativaContatoMap : EntityTypeConfiguration<SYS_UnidadeAdministrativaContato>
    {
        public SYS_UnidadeAdministrativaContatoMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ent_id, t.uad_id, t.uac_id });

            // Properties
            this.Property(t => t.uac_contato)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("SYS_UnidadeAdministrativaContato");
            this.Property(t => t.ent_id).HasColumnName("ent_id");
            this.Property(t => t.uad_id).HasColumnName("uad_id");
            this.Property(t => t.uac_id).HasColumnName("uac_id");
            this.Property(t => t.tmc_id).HasColumnName("tmc_id");
            this.Property(t => t.uac_contato).HasColumnName("uac_contato");
            this.Property(t => t.uac_situacao).HasColumnName("uac_situacao");
            this.Property(t => t.uac_dataCriacao).HasColumnName("uac_dataCriacao");
            this.Property(t => t.uac_dataAlteracao).HasColumnName("uac_dataAlteracao");

            // Relationships
            this.HasRequired(t => t.SYS_TipoMeioContato)
                .WithMany(t => t.SYS_UnidadeAdministrativaContato)
                .HasForeignKey(d => d.tmc_id);
            this.HasRequired(t => t.SYS_UnidadeAdministrativa)
                .WithMany(t => t.SYS_UnidadeAdministrativaContato)
                .HasForeignKey(d => new { d.ent_id, d.uad_id });

        }
    }
}
