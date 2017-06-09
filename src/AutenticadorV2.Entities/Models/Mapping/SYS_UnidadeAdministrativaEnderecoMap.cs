using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class SYS_UnidadeAdministrativaEnderecoMap : EntityTypeConfiguration<SYS_UnidadeAdministrativaEndereco>
    {
        public SYS_UnidadeAdministrativaEnderecoMap()
        {
            // Primary Key
            this.HasKey(t => new { t.ent_id, t.uad_id, t.uae_id });

            // Properties
            this.Property(t => t.uae_numero)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.uae_complemento)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("SYS_UnidadeAdministrativaEndereco");
            this.Property(t => t.ent_id).HasColumnName("ent_id");
            this.Property(t => t.uad_id).HasColumnName("uad_id");
            this.Property(t => t.uae_id).HasColumnName("uae_id");
            this.Property(t => t.end_id).HasColumnName("end_id");
            this.Property(t => t.uae_numero).HasColumnName("uae_numero");
            this.Property(t => t.uae_complemento).HasColumnName("uae_complemento");
            this.Property(t => t.uae_situacao).HasColumnName("uae_situacao");
            this.Property(t => t.uae_dataCriacao).HasColumnName("uae_dataCriacao");
            this.Property(t => t.uae_dataAlteracao).HasColumnName("uae_dataAlteracao");

            // Relationships
            this.HasRequired(t => t.END_Endereco)
                .WithMany(t => t.SYS_UnidadeAdministrativaEndereco)
                .HasForeignKey(d => d.end_id);
            this.HasRequired(t => t.SYS_UnidadeAdministrativa)
                .WithMany(t => t.SYS_UnidadeAdministrativaEndereco)
                .HasForeignKey(d => new { d.ent_id, d.uad_id });

        }
    }
}
