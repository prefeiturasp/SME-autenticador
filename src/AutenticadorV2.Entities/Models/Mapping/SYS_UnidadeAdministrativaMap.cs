using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_UnidadeAdministrativaMap : EntityTypeConfiguration<SYS_UnidadeAdministrativa>
    {
        public SYS_UnidadeAdministrativaMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ent_id, t.uad_id });

            // Properties
            this.Property(t => t.uad_codigo)
                .HasMaxLength(20);

            this.Property(t => t.uad_nome)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.uad_sigla)
                .HasMaxLength(50);

            this.Property(t => t.uad_codigoIntegracao)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("SYS_UnidadeAdministrativa");
            this.Property(t => t.ent_id).HasColumnName("ent_id");
            this.Property(t => t.uad_id).HasColumnName("uad_id");
            this.Property(t => t.tua_id).HasColumnName("tua_id");
            this.Property(t => t.uad_codigo).HasColumnName("uad_codigo");
            this.Property(t => t.uad_nome).HasColumnName("uad_nome");
            this.Property(t => t.uad_sigla).HasColumnName("uad_sigla");
            this.Property(t => t.uad_idSuperior).HasColumnName("uad_idSuperior");
            this.Property(t => t.uad_situacao).HasColumnName("uad_situacao");
            this.Property(t => t.uad_dataCriacao).HasColumnName("uad_dataCriacao");
            this.Property(t => t.uad_dataAlteracao).HasColumnName("uad_dataAlteracao");
            this.Property(t => t.uad_integridade).HasColumnName("uad_integridade");
            this.Property(t => t.uad_codigoIntegracao).HasColumnName("uad_codigoIntegracao");

            // Relationships
            this.HasRequired(t => t.SYS_Entidade)
                .WithMany(t => t.SYS_UnidadeAdministrativa)
                .HasForeignKey(d => d.ent_id);
            this.HasRequired(t => t.SYS_TipoUnidadeAdministrativa)
                .WithMany(t => t.SYS_UnidadeAdministrativa)
                .HasForeignKey(d => d.tua_id);
            this.HasOptional(t => t.SYS_UnidadeAdministrativa2)
                .WithMany(t => t.SYS_UnidadeAdministrativa1)
                .HasForeignKey(d => new { d.ent_id, d.uad_idSuperior });

        }
    }
}
