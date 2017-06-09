using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AutenticadorV2.Entities.Models.Mapping
{
    public class END_UnidadeFederativaMap : EntityTypeConfiguration<END_UnidadeFederativa>
    {
        public END_UnidadeFederativaMap()
        {
            // Primary Key
            this.HasKey(t => t.unf_id);

            // Properties
            this.Property(t => t.unf_nome)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.unf_sigla)
                .IsRequired()
                .HasMaxLength(2);

            // Table & Column Mappings
            this.ToTable("END_UnidadeFederativa");
            this.Property(t => t.unf_id).HasColumnName("unf_id");
            this.Property(t => t.pai_id).HasColumnName("pai_id");
            this.Property(t => t.unf_nome).HasColumnName("unf_nome");
            this.Property(t => t.unf_sigla).HasColumnName("unf_sigla");
            this.Property(t => t.unf_situacao).HasColumnName("unf_situacao");
            this.Property(t => t.unf_integridade).HasColumnName("unf_integridade");

            // Relationships
            this.HasRequired(t => t.END_Pais)
                .WithMany(t => t.END_UnidadeFederativa)
                .HasForeignKey(d => d.pai_id);

        }
    }
}
